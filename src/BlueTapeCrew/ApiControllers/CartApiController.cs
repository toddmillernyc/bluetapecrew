﻿using System;
using BlueTapeCrew.Services;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;
using System.Threading.Tasks;
using BlueTapeCrew.Extensions;

namespace BlueTapeCrew.ApiControllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartApiController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ISessionService _session;

        public CartApiController(
            ICartService cartService,
            ISessionService session)
        {
            _cartService = cartService;
            _session = session;
        }

        [HttpPost]
        [Route("{styleId}/{quantity}")]
        public async Task<IActionResult> Post(int styleId, int quantity)
        {
            try
            {
                await _cartService.AddOrUpdate(new Cart
                {
                    SessionId = _session.SessionId(),
                    StyleId = styleId,
                    Quantity = quantity
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToInnerExceptionMessage());
            }
        }
    }
}
