using ServiceLayer.DTOs;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using TMS.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace TMS.Controllers
{
    [Authorize(Roles = "User")]
    [RoutePrefix("api/quotes")]
    public class QuoteController : ApiController
    {
        public IQuoteService quoteService;
        private readonly Mapper quoteMapper;

        public QuoteController()
        {
            quoteService = new QuoteService();
            var quoteConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<QuoteDTO, QuoteVM>();
                cfg.CreateMap<QuoteVM, QuoteDTO>();
            });
            quoteMapper = new Mapper(quoteConfig);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetQuote(int id)
        {
            try
            {
                var quoteVM = quoteMapper.Map<QuoteVM>(quoteService.GetQuote(id));
                if(quoteVM == null)
                {
                    return NotFound();
                }
                return Json(quoteVM);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Fetching Quote {id}: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetQuotes()
        {
            try
            {
                var quoteVMs = quoteMapper.Map<List<QuoteVM>>(quoteService.GetQuotes());
                return Json(quoteVMs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Fetching Quotes: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostQuote(QuoteVM quoteVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var quoteDTO = quoteMapper.Map<QuoteDTO>(quoteVM);
                quoteDTO.QuoteID = quoteService.GetNextAvailableID();
                quoteService.AddQuote(quoteDTO);
                return Ok(new { message = "Quote Added Successfully!",
                    quote = new
                    {
                        quoteDTO.QuoteID,
                        quoteDTO.QuoteType,
                        quoteDTO.Description,
                        quoteDTO.DueDate,
                        quoteDTO.Premium,
                        quoteDTO.Sales
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Adding New Quote: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult PutQuote(int id, QuoteVM quoteVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!QuoteExists(id))
            {
                return Content(HttpStatusCode.BadRequest, new { message = $"Quote {id} does not exist" });
            }

            try
            {
                var quoteDTO = quoteMapper.Map<QuoteDTO>(quoteVM);
                quoteService.EditQuote(id, quoteDTO);
                return Ok(new { message = "Quote Updated Successfully!", quote = quoteDTO });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Concurrency error updating quote with ID {id}: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteQuote(int id)
        {
            try
            {
                quoteService.RemoveQuote(id);
                return Ok(new { message = $"Quote {id} Deleted Successfully!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Deleting Quote {id}: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        private bool QuoteExists(int id)
        {
            var quoteVM = quoteMapper.Map<QuoteVM>(quoteService.GetQuote(id));
            return quoteVM != null;
        }
    }
}