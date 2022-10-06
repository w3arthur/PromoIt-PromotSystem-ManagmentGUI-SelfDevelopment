﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromotItLibrary.Models;
using PromotItLibrary.Patterns;
using PromotItLibrary.Patterns.Actions;
using PromotItLibrary.Patterns.DataTables;
using PromotItLibrary.Patterns.LinkedLists;

namespace PromotItLibrary.Classes
{
    public class ProductDonated
    {
        private static MySQL mySQL = Configuration.MySQL;
        private HTTPClient httpClient = Configuration.HTTPClient;

        private ActionsProduct actionsProduct;
        private LinkedListProduct linkedListProduct;
        private DataTabletProduct dataTabletProduct;

        public ProductInCampaign ProductInCampaign { get; set; }
        public Users ActivistUser { get; set; }
        public string Quantity { get; set; }
        public string Shipped { get; set; }
        public string Id { get; set; }

        public ProductDonated()
        {
            actionsProduct = new ActionsProduct(this, null, mySQL, httpClient);
            linkedListProduct = new LinkedListProduct(this, null, mySQL, httpClient);
            dataTabletProduct = new DataTabletProduct(this, null);
        }


        //Actions
        public async Task SetTwitterMessagTweet_SetBuyAnItemAsync() =>
            await actionsProduct.SetTwitterMessagTweet_SetBuyAnItemAsync();
        public async Task<bool> SetBuyAnItemAsync(Modes mode = null) =>
            await actionsProduct.SetBuyAnItemAsync(mode);
        public async Task<bool> SetProductShippingAsync(Modes mode = null) =>
            await actionsProduct.SetProductShippingAsync(mode);
        public async Task<bool> SetNewProductAsync(Modes mode = null) =>
            await actionsProduct.SetNewProductAsync(mode);

        // LinkedList and DataTable
        public async Task<List<ProductDonated>> MySQL_GetDonatedProductForShipping_ListAsync(Modes mode = null) =>
             await linkedListProduct.MySQL_GetDonatedProductForShipping_ListAsync(mode);
        public async Task<DataTable> GetDonatedProductForShipping_DataTableAsync() =>
             await dataTabletProduct.GetDonatedProductForShipping_DataTableAsync();
        public async Task<DataTable> GetList_DataTableAsync() =>
             await dataTabletProduct.GetList_DataTableAsync();
        public async Task<List<ProductInCampaign>> MySQL_GetProductList_ListAsync(Modes mode = null) =>
             await linkedListProduct.MySQL_GetProductList_ListAsync(mode);

    }
}
