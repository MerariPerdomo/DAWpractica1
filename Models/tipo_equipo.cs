﻿using System.ComponentModel.DataAnnotations;

namespace DAWpractica1.Models
{
    public class tipo_equipo
    {
        [Key]
        public int? id_tipo_equipo { get; set; }
        public string? descripcion { get; set; }
        public char estado { get; set; }
    }
}
