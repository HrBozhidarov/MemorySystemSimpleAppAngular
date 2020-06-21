﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemorySystemApp.Data.Models
{
    public class Like
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int PictureId { get; set; }

        public Picture Picture { get; set; }
    }
}
