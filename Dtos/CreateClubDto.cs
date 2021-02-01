using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Entities;

namespace API.Dtos
{
    public class CreateClubDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}