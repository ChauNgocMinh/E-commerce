﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Response.MovieResponse
{
    public class MovieDetailViewModel
    {
        public MovieResponse Movie { get; set; }
        public List<MovieFeedBackResponse> Feedbacks { get; set; }
    }
}
