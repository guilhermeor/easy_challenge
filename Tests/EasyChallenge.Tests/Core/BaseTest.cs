using EasyChallenge.Domain;
using EasyChallenge.Domain.Constants;
using System;
using System.Collections.Generic;

namespace EasyChallenge.Tests.Core
{
    public class BaseTest
    {
        public static IEnumerable<object[]> PurchaseAndDueDatesTo6Percent()
        {
            return new List<object[]>
            {
                new object[] {  new Funds() { TotalValue = 10, PurchaseDate = DateTime.Now.AddMonths(-10), DueDate = DateTime.Now.AddMonths(2)} },
                new object[] {  new Funds() { TotalValue = 10, PurchaseDate = DateTime.Now.AddMonths(-1), DueDate = DateTime.Now.AddMonths(1)} },
                new object[] {  new Funds() { TotalValue = 10, PurchaseDate = DateTime.Now, DueDate = DateTime.Now.AddMonths(2)} },
                new object[] {  new Lci() { TotalValue = 10, PurchaseDate = DateTime.Now.AddMonths(-10), DueDate = DateTime.Now.AddMonths(2)} },
                new object[] {  new Lci() { TotalValue = 10, PurchaseDate = DateTime.Now.AddMonths(-1), DueDate = DateTime.Now.AddMonths(1)} },
                new object[] {  new Lci() { TotalValue = 10, PurchaseDate = DateTime.Now, DueDate = DateTime.Now.AddMonths(2)} },
                new object[] {  new Td() { TotalValue = 10, PurchaseDate = DateTime.Now.AddMonths(-10), DueDate = DateTime.Now.AddMonths(2)} },
                new object[] {  new Td() { TotalValue = 10, PurchaseDate = DateTime.Now.AddMonths(-1), DueDate = DateTime.Now.AddMonths(1)} },
                new object[] {  new Td() { TotalValue = 10, PurchaseDate = DateTime.Now, DueDate = DateTime.Now.AddMonths(2)} },
            };
        }
        public static IEnumerable<object[]> PurchaseAndDueDatesTo15Percent()
        {
            return new List<object[]>
            {
                new object[] {  new Funds() { TotalValue = 10, PurchaseDate = DateTime.Now.AddMonths(-10), DueDate = DateTime.Now.AddMonths(4)} },
                new object[] {  new Lci() { TotalValue = 10, PurchaseDate = DateTime.Now.AddMonths(-10), DueDate = DateTime.Now.AddMonths(4)} },
                new object[] {  new Td() { TotalValue = 10, PurchaseDate = DateTime.Now.AddMonths(-10), DueDate = DateTime.Now.AddMonths(4)} },
            };
        }
        public static IEnumerable<object[]> PurchaseAndDueDatesTo30Percent()
        {
            return new List<object[]>
            {
                new object[] {  new Funds() { TotalValue = 10, PurchaseDate = DateTime.Now.AddMonths(-10), DueDate = DateTime.Now.AddMonths(12)} },
                new object[] {  new Lci() { TotalValue = 10, PurchaseDate = DateTime.Now.AddMonths(-10), DueDate = DateTime.Now.AddMonths(12)} },
                new object[] {  new Td() { TotalValue = 10, PurchaseDate = DateTime.Now.AddMonths(-10), DueDate = DateTime.Now.AddMonths(12)} },
            };
        }

        public static IEnumerable<object[]> DataProfitabilityPositive()
        {
            return new List<object[]>
            {
                new object[] { new Funds() { InvestedAmount = 10, TotalValue = 15}, IRPercents.FUNDS},
                new object[] { new Funds() { InvestedAmount = 10.23M, TotalValue = 15.96M }, IRPercents.FUNDS},
                new object[] { new Lci() { InvestedAmount = 10, TotalValue = 15 }, IRPercents.LCIS},
                new object[] { new Lci() { InvestedAmount = 10.23M, TotalValue = 15.96M }, IRPercents.LCIS},
                new object[] { new Td() { InvestedAmount = 10, TotalValue = 15 }, IRPercents.TDS},
                new object[] { new Td() { InvestedAmount = 10.23M, TotalValue = 15.96M }, IRPercents.TDS},
            };
        }
        public static IEnumerable<object[]> DataProfitabilityZero()
        {
            return new List<object[]>
            {
                new object[] { new Funds() { InvestedAmount = 10, TotalValue = 0}},
                new object[] { new Funds() { InvestedAmount = 8.23M, TotalValue = 3.45M }},
                new object[] { new Lci() { InvestedAmount = 10, TotalValue = 0 }},
                new object[] { new Lci() { InvestedAmount = 8.23M, TotalValue = 3.45M }},
                new object[] { new Td() { InvestedAmount = 10, TotalValue = 0 }},
                new object[] { new Td() { InvestedAmount = 8.23M, TotalValue = 3.45M }},
            };
        }
    }
}