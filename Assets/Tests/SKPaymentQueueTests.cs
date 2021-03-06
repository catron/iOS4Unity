﻿using iOS4Unity;
using NUnit.Framework;
using System;

public class SKPaymentQueueTests
{
    [Test]
    public void DefaultQueue()
    {
        var queue = SKPaymentQueue.DefaultQueue;

        Assert.AreNotEqual(IntPtr.Zero, queue.Handle);
    }

    [Test]
    public void DefaultQueueDispose()
    {
        var queue = SKPaymentQueue.DefaultQueue;
        queue.Dispose();
    }

    [Test]
    public void ObjectSame()
    {
        var a = SKPaymentQueue.DefaultQueue;
        var b = SKPaymentQueue.DefaultQueue;
        Assert.AreSame(a, b);
    }

    [Test]
    public void CanMakePayments()
    {
        //Just make sure there isn't a crash
        SKPaymentQueue.CanMakePayments.ToString();
    }

    [Test]
    public void Transactions()
    {
        var transactions = SKPaymentQueue.DefaultQueue.Transactions;
        Assert.AreEqual(0, transactions.Length);
    }

    [Test]
    public void RestoreCompletedTransactions()
    {
        var queue = SKPaymentQueue.DefaultQueue;
        queue.RestoreCompleted += (sender, e) =>
        {
            Console.WriteLine("Restore completed!");
        };
        queue.RestoreFailed += (sender, e) =>
        {
            Console.WriteLine("Restore failed: " + e.Error.LocalizedDescription);
        };

        queue.RestoreCompletedTransactions();
    }
}