using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Entities;

namespace API.Dtos
{
    public class CreateClubDto
    {
        public string Name { get; set; }
        public string Intro { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Events { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}