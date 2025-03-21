using System;
using System.Collections.Generic;
using System.Linq;

namespace BitcoinWalletManagementSystem
{
    public class BitcoinWalletManager : IBitcoinWalletManager
    {
        private Dictionary<string, User> usersById = new Dictionary<string, User>();
        private Dictionary<string, Wallet> walletsById = new Dictionary<string, Wallet>();

        public void CreateUser(User user)
        {
            this.usersById.Add(user.Id, user);
        }

        public void CreateWallet(Wallet wallet)
        {
            this.walletsById.Add(wallet.Id, wallet);
        }

        public bool ContainsUser(User user)
        {
            return this.usersById.ContainsKey(user.Id);
        }

        public bool ContainsWallet(Wallet wallet)
        {
            return this.walletsById.ContainsKey(wallet.Id);
        }

        public IEnumerable<Wallet> GetWalletsByUser(string userId)
        {
            return this.walletsById.Values.Where(w => w.UserId == userId);
        }

        public void PerformTransaction(Transaction transaction)
        {
            if (!this.walletsById.ContainsKey(transaction.SenderWalletId) || !this.walletsById.ContainsKey(transaction.ReceiverWalletId))
            {
                throw new ArgumentException();
            }

            Wallet senderWallet = this.walletsById[transaction.SenderWalletId];
            Wallet receiverWallet = this.walletsById[transaction.ReceiverWalletId];

            senderWallet.Balance -= transaction.Amount;
            receiverWallet.Balance += transaction.Amount;

            this.usersById[senderWallet.UserId].Transactions.Add(transaction);
            this.usersById[receiverWallet.UserId].Transactions.Add(transaction);

            this.usersById[senderWallet.UserId].OverallBalance += senderWallet.Balance;
            this.usersById[receiverWallet.UserId].OverallBalance += receiverWallet.Balance;
        }

        public IEnumerable<Transaction> GetTransactionsByUser(string userId)
        {
            if (!this.usersById.ContainsKey(userId))
            {
                throw new ArgumentException();
            }

            return this.usersById[userId].Transactions;
        }

        public IEnumerable<Wallet> GetWalletsSortedByBalanceDescending()
        {
            return this.walletsById.Values.OrderByDescending(w => w.Balance);
        }

        public IEnumerable<User> GetUsersSortedByBalanceDescending()
        {
            return this.usersById.Values.OrderByDescending(u => u.OverallBalance);
        }

        public IEnumerable<User> GetUsersByTransactionCount()
        {
            return this.usersById.Values.OrderByDescending(u => u.Transactions.Count());
        }
    }
}