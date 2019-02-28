﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Models
{
    public class Matchs
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string StandardDate { get; set; }
        public int Team1 { get; set; }
        public int Team2 { get; set; }
        public string Team1Name { get; set; }
        public string Team2Name { get; set; }
        public string Team1Flag { get; set; }
        public string Team2Flag { get; set; }
        public int? Team1Goals { get; set; }
        public int? Team2Goals { get; set; }
        public string Group { get; set; }
        public int? Team1Forecast { get; set; }
        public int? Team2Forecast { get; set; }
        public int Points { get; set; }
        public bool CanUpdate { get; set; } = false;
        public bool Closed { get; set; }
    }
}
