using MyTicTacToe.MVVM.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTicTacToe.Services
{
    public class ItemDatabase
    {
        SQLiteAsyncConnection dbConnection;
        public ItemDatabase()
        {
        }
        async Task Init()
        {
            if (dbConnection is not null)
                return;

            SQLitePCL.Batteries.Init();
            dbConnection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await dbConnection.CreateTableAsync<Player>();
        }

        public async Task<List<Player>> GetPlayersAsync()
        {
            await Init();
            return await dbConnection.Table<Player>().ToListAsync();
        }

        //public async Task<int> SaveResultAsync(Player player)
        //{
        //    //await Init();
        //    //if (player.ID != 0)
        //    //{
        //    //    return await dbConnection.UpdateAsync(player);
        //    //}
        //    //else
        //    //{
        //    //    return await dbConnection.InsertAsync(player);
        //    //}

        //}

        /// <summary>
        /// Using SQL
        /// </summary>
        /// <returns></returns>
        public async Task<List<Player>> GetItemsAsyncWithSQL(Player player)
        {
            await Init();

            //sql version
            return await dbConnection.QueryAsync<Player>("SELECT * FROM [Player] WHERE [Namer] = " + player.Name);

            //linq version
            //return await dbConnection.Table<Item>().Where(item => item.Price == 42).ToListAsync();
        }
    }
}

