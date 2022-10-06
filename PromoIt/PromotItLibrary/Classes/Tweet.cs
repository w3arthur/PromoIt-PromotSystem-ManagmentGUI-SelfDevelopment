﻿using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using PromotItLibrary.Models;
using PromotItLibrary.Patterns;
using PromotItLibrary.Patterns.Actions;
using PromotItLibrary.Patterns.DataTables;
using PromotItLibrary.Patterns.LinkedLists;
using System;
using System.Collections.Generic;

using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Models;

namespace PromotItLibrary.Classes
{
    public class Tweet
    {
        private static MySQL mySQL = Configuration.MySQL;
        private HTTPClient httpClient = Configuration.HTTPClient;

        private LinkedListTweet linkedListTweet;
        private DataTableTweet dataTableTweet;
        private ActionsTweet actionsTweet;

        public string Id { get; set; }
        public Campaign Campaign { get; set; }
        public Users ActivistUser { get; set; }
        public decimal Cash { get; set; }
        public int Retweets { get; set; }
        public bool IsApproved { get; set; }

        public Tweet()
        {
            actionsTweet = new ActionsTweet( this, mySQL, httpClient);
            linkedListTweet = new LinkedListTweet(this, mySQL, httpClient);
            dataTableTweet = new DataTableTweet(this);
        }

        //Actions
        public async Task<bool> SetTweetCashAsync(Modes mode = null) =>
            await actionsTweet.SetTweetCashAsync(mode);

        //LinkedList and DataTable
        public async Task<List<Tweet>> MySQL_GetAllTweets_ListAsync(Modes mode = null) =>
            await linkedListTweet.MySQL_GetAllTweets_ListAsync(mode);
        public async Task<DataTable> GetAllTweets_DataTableAsync() =>
            await dataTableTweet.GetAllTweets_DataTableAsync();


    }
}
