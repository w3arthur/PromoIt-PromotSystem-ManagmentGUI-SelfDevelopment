﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PromotItLibrary.Models;
using PromotItLibrary.Patterns.Actions;

namespace PromotItLibrary.Interfaces
{
    public interface INonProfitUser
    {
        string Email { get; set; }
        string WebSite { get; set; }
    }
}