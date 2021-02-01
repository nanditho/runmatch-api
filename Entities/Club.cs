using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Entities;

namespace API.Domain
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}