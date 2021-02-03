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
        public string Intro { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Events { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;        
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}