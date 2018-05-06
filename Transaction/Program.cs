using System;
using System.Collections.Generic;
using Transaction.model;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;

namespace Transaction
{
    class Program
    {
        static void addLike(Photo photo)
        {
            using (PhotoContext db = new PhotoContext())
            {                
                if (photo == null)
                {
                    return;
                }
                Thread.Sleep(2000);
                photo.Likes++;
                db.Entry(photo).State = EntityState.Modified;
                
                db.SaveChanges();
            }

        }
        static void Main(string[] args)
        {
            using (PhotoContext db = new PhotoContext())
            {
                Photo photo1 = new Photo { Likes = 20 };
                db.Photos.Add(photo1);
                db.SaveChanges();

                //Task task1 = new Task(() => addLike(photo1));
                //Task task2 = new Task(() => addLike(photo1));
                //Task task3 = new Task(() => addLike(photo1));

                Task[] tasks = new Task[10];
                int j = 1;
                for (int i = 0; i < tasks.Length; i++)
                    tasks[i] = Task.Factory.StartNew(() =>
                    {
                        Console.WriteLine($"Task {j++}");
                        addLike(photo1);
                    });
                /*task1.Start();
                task2.Start();*/
                Task.WaitAll();
                //task1.Wait();
                //task2.Wait();
                

                db.SaveChanges();
            }
            Console.Read();
        }
    }
}