﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PromotItLibrary.Models;
using PromotItLibrary.Patterns.Actions;

namespace PromotItLibrary.Classes
{
    public class ActivistUser : Users
    {
        public static string CashDefultSet { get; } = "1000.0"; //set to 0

        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Cash { get; set; }

        public ActivistUser() : base() { UserType = "activist"; actionsUser = new ActionsUser(this, mySQL, httpClient); }
        public ActivistUser(Users user) : base(user) { UserType = "activist"; actionsUser = new ActionsUser(this, mySQL, httpClient); }

        //Actions
        public async Task<ActivistUser> GetCashAmountAsync(Modes mode = null) =>
            await actionsUser.GetCashAmountAsync(mode);
    }
}
