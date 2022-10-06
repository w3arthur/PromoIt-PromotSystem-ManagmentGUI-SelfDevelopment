﻿using PromotItLibrary.Classes;
using PromotItLibrary.Models;
using PromotItLibrary.Patterns.Actions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotItLibrary.Patterns.Actions.Queue_State
{
    public class ActionsTweet_Queue : IActionsTweet
    {
        private static MySQL mySQL;
        private HTTPClient httpClient;

        private Tweet _tweet;

        public ActionsTweet_Queue(Tweet tweet, MySQL _mySQL, HTTPClient _httpClient)
        {
            mySQL = _mySQL;
            httpClient = _httpClient;
            _tweet = tweet;
        }

        public async Task<bool> SetTweetCashAsync()
        {
            return (bool)await httpClient.PostSingleDataInsert(Configuration.PromoitTweetQueue, _tweet, "SetTweetCash");
        }
    }
}
