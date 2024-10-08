﻿using System;
using System.Security.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BankingProjectTests
{
    [TestClass]
    public class AccountMemoryRepoTests
    {
        private AccountMemoryRepo _repo;
        [TestInitialize]
        public void initialize()
        {
            _repo = AccountMemoryRepo.Instance;
        }
        [TestMethod]
        public void Test_ReadAllAccounts()
        {
            _repo = AccountMemoryRepo.Instance;
            var accounts = _repo.ReadAllAccount();

            Assert.AreEqual(2, accounts.Count);
            Assert.IsTrue(accounts.Any(a => a.AccountNumber == 1234));
            Assert.IsTrue(accounts.Any(a => a.AccountNumber == 12345));
        }

        [TestMethod]
        public void Test_CreateAccount()
        {
            var account = new AccountModel()
            {
                AccountNumber = 49,
                Name = "Rishwin",
                Balance = 0,
                Type = "current",
                Email = "rishwin@gmail.com",
                PhoneNumber = "5236526526",
                Address = "address",
                IsActive = true,
                InterestPercentage = "0",
                TransactionCount = 0,
                LastTransactionDate = DateTime.Now,
            };
            _repo.Create(account);

            Assert.IsTrue(_repo.ReadAllAccount().Any(ac => ac.AccountNumber == 49));
        }

        [TestMethod]
        public void Test_UpdateAccount()
        {
            Account account = new Account
            {
                AccountNumber = 1234,
                Name = "Minnu",
                Balance = 0,
                Type = "savings",
                Email = "minnu@gmail.com",
                PhoneNumber = "5236526526",
                Address = "Address",
                IsActive = true,
                InterestPercentage = "0",
                TransactionCount = 0,
                LastTransactionDate = DateTime.Now,
            };

            account.Address = "New Address";
            _repo.UpdateAccount(account);
            Assert.AreEqual("New Address", _repo.ReadAllAccount().First(a => a.AccountNumber == 1234).Address);
        }
        [TestMethod]
        public void Test_Deposit()
        {
            Account account = new Account
            {
                AccountNumber = 1234,
                Name = "Minnu",
                Balance = 0,
                Type = "savings",
                Email = "minnu@gmail.com",
                PhoneNumber = "5236526526",
                Address = "Address",
                IsActive = true,
                InterestPercentage = "0",
                TransactionCount = 0,
                LastTransactionDate = DateTime.Now,
            };

            _repo.Deposit(account.AccountNumber, 1000);
            Assert.AreEqual(1000, _repo.ReadAllAccount().First(ac => ac.AccountNumber == account.AccountNumber).Balance);
        }

        [TestMethod]
        public void Test_Withdraw()
        {
            var account = new Account()
            {
                AccountNumber = 12345,
                Name = "Ashna",
                Balance = 0,
                Type = "current",
                Email = "ashna@gmail.com",
                PhoneNumber = "5236526526",
                Address = "address",
                IsActive = true,
                InterestPercentage = "0",
                TransactionCount = 0,
                LastTransactionDate = DateTime.Now,
            };

            _repo.Deposit(account.AccountNumber, 1000);
            _repo.Withdrw(account.AccountNumber, 700);
            Assert.AreEqual(300, _repo.ReadAllAccount().First(ac => ac.AccountNumber == account.AccountNumber).Balance);

        }

    }
}
