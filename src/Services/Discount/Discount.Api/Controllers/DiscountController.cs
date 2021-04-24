using Discount.Api.Entities;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discount.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository repository;

        public DiscountController(IDiscountRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{productName}", Name ="GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK )]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            return Ok(await this.repository.GetDiscount(productName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody]Coupon coupon)
        {
            await this.repository.CreateDiscount(coupon);
            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }
        
        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateDiscount([FromBody]Coupon coupon)
        {
            return Ok(await this.repository.UpdateDiscount(coupon));
        }
        
        [HttpDelete("{productName}", Name ="DeleteDiscount")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteDiscount(string productName)
        {
            return Ok(await this.repository.DeleteDiscount(productName));
        }




    }
}
