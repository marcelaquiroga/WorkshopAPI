using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WorkshopApp.DTOs;
using WorkshopApp.Models;

namespace WorkshopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly WorkshopDBContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CustomersController(WorkshopDBContext context,
            IMapper mapper,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        
        //POST: api/Customers/Login
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] dynamic LoginInfo)
        {
            var userName = (string)LoginInfo["username"];
            var password = (string)LoginInfo["password"];

            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.UserName == userName && m.Password == password);

            if (customer != null)
            {
                return Ok(new { token = CreateToken(customer)});
            }
            else
            {
                return Unauthorized();
            }
        }

        private string CreateToken(Customer userInfo)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                );

            var signingCredentials = new SigningCredentials(
                    symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(signingCredentials);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, userInfo.CustomerId.ToString()),
                new Claim("nombre", userInfo.Name),
                new Claim("apellidos", userInfo.LastName)
            };

            var payload = new JwtPayload(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(2)
                );

            var token = new JwtSecurityToken(
                    header,
                    payload
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        // GET: api/Customers
        [HttpGet]
        //[Authorize]
        public async Task<IEnumerable<CustomerDTO>> GetCustomer()
        {
            var model = await _context.Customer.ToListAsync();
            return  _mapper.Map<IEnumerable<CustomerDTO>>(model);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            var result = _mapper.Map<CustomerDTO>(customer);
            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerDTO>> DeleteCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return  _mapper.Map<CustomerDTO>(customer);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerId == id);
        }
    }
}
