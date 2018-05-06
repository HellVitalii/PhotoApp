using System;
using System.Collections.Generic;
using Transaction.model;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;
using System.Data;

namespace Transaction
{
    class Program
    {
        static void addLike(Photo photo)
        {
            using (PhotoContext db = new PhotoContext())
            {
                using (var transaction = db.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    try
                    {

                        if (photo == null)
                        {
                           return;
                        }
                        Thread.Sleep(5000);
                        photo.Likes++;
                        db.Entry(photo).State = EntityState.Modified;
                
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception {0}", ex);
                    }

                }
            }

        }
        static void Main(string[] args)
        {
            using (PhotoContext db = new PhotoContext())
            {
                Photo photo1 = new Photo { Likes = 20 };
                db.Photos.Add(photo1);
                db.SaveChanges();

                
                Task[] tasks = new Task[10];
                int j = 1;
                for (int i = 0; i < tasks.Length; i++)
                    tasks[i] = Task.Factory.StartNew(() =>
                    {
                        Console.WriteLine($"Task {j++}");
                        addLike(photo1);
                    });
               
                Task.WaitAll();
               

                db.SaveChanges();
            }
            Console.Read();
        }
    }
}