﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;

namespace WebStore.WebAPI.Controllers.Identity
{
    [Route(WebAPIAddress.Identity.Users)]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly UserStore<User, Role, WebStoreDB> _UserStore;

        public UsersApiController(WebStoreDB db) 
        {
            _UserStore = new UserStore<User, Role, WebStoreDB>(db);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<User>> GetAllUsers() => await _UserStore.Users.ToArrayAsync();
    }
}