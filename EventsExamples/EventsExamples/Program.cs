namespace EventsExamples
{
    internal class Program
    {
        public class Video 
        {
         public string Title { get; }
         public string Description { get; }
         public Video(string Title,string Description) { this.Title = Title; this.Description = Description; }

        }
        public class Post
        {
            public string Title { get; }
            public string Content { get; }
            public Post(string Title, string Content) { this.Title = Title; this.Content = Content; }

        }
        public class YoutubeChannle
        {
            public event EventHandler<Video> NewVideoPublished;
            public event EventHandler<Post> NewPostPublished;
            public string Name { get; }
            public YoutubeChannle(string Name) { this.Name = Name; }
            public void PublishAVideo(string Title ,string Description) 
            {
                RaiseOnNewVideoPublished( new Video( Title, Description));
            }
            protected virtual void RaiseOnNewVideoPublished(Video v)
            {
                NewVideoPublished?.Invoke(this, v);
            }
            public void PublishAPost(string Title, string Content)
            {
                RaiseOnNewPostPublished(new Post(Title, Content));
            }
            protected virtual void RaiseOnNewPostPublished(Post p)
            {
                NewPostPublished?.Invoke(this, p);
            }

        }
        public class Subscriber
        {
            public string FullName { get; } 
            public DateTime BirthDate { get;  }
            public Subscriber(string FullName , DateTime BirthDate) 
            {
                this.FullName = FullName; this.BirthDate = BirthDate;
            }
            public void SubscribeAVideo(YoutubeChannle Channle) 
            {
                Channle.NewVideoPublished += ShowNotificationOnNewVideo;
                
            }
            public void UnSubscribeAVideo(YoutubeChannle Channle)
            {
                Channle.NewVideoPublished -= ShowNotificationOnNewVideo;
                Console.WriteLine($"\nSubscriber {FullName} Has UnSubscribe The Channle Of {Channle.Name} For Videos");
            }
            public void SubscribeAPost(YoutubeChannle Channle)
            {
                Channle.NewPostPublished += ShowNotificationOnPost;
            }
            public void UnSubscribeAPost(YoutubeChannle Channle)
            {
                Channle.NewPostPublished -= ShowNotificationOnPost;
                Console.WriteLine($"\nSubscriber {FullName} Has UnSubscribe The Channle Of {Channle.Name} for Posts");
            }
            public void  ShowNotificationOnNewVideo(object sender,Video v) 
            {
                Console.WriteLine($"\nSubscriber Name {FullName}");
                Console.WriteLine($"\nNew Video Published By Channle ({((YoutubeChannle)sender).Name}) \n Title : {v.Title}\n Description:{v.Description}");
            }
            public void ShowNotificationOnPost(object sender, Post P)
            {
                Console.WriteLine($"\nSubscriber Name {FullName}");
                Console.WriteLine($"\nNew post Published By Channle({((YoutubeChannle)sender).Name}) \n Title : {P.Title}\n Content:{P.Content}");
            }

        }
        public class Archive 
        {
        public string Name { get; }
        public Archive(string name)
            {
                this.Name = name;
            }
        
        public void StartVideoArchive(YoutubeChannle Channle) 
        {
                Channle.NewVideoPublished += SendVideoToArchive;
        }
        public void StopVideoArchive(YoutubeChannle Channle)
        {
                Channle.NewVideoPublished -= SendVideoToArchive;
        }
            public void StartPostArchive(YoutubeChannle Channle)
            {
                Channle.NewPostPublished += SendPostToArchive;
            }
            public void StopPostArchive(YoutubeChannle Channle)
            {
                Channle.NewPostPublished -= SendPostToArchive;
            }

            public void SendVideoToArchive(object sender,Video video)
        {
           File.AppendAllText($@"C:\\{((YoutubeChannle)sender).Name} Videos.txt",$"Publisher:{((YoutubeChannle)sender).Name}\nTitle:{video.Title}\nDescription:{video.Description}\nDate:{DateTime.Now.ToShortDateString()}\n\n");
        }
        public void SendPostToArchive(object sender, Post P)
        {
                File.AppendAllText($@"C:\\{((YoutubeChannle)sender).Name} Posts.txt", $"Publisher:{((YoutubeChannle)sender).Name}\nTitle:{P.Title}\nContent:{P.Content}\nDate:{DateTime.Now.ToShortDateString()}\n\n");
        }


        }
        static void Main(string[] args)
        {
            YoutubeChannle ProgrammingChannle = new YoutubeChannle("ProgrammingAdvices");
            Subscriber Mortdajabbar = new Subscriber("Mortda Jabbar Hassan", new DateTime(1985, 6, 6));
            Archive MyArchive = new Archive("Programming Advces Archive");
            Mortdajabbar.SubscribeAVideo(ProgrammingChannle);
            Mortdajabbar.SubscribeAPost(ProgrammingChannle);
            MyArchive.StartVideoArchive(ProgrammingChannle);
            MyArchive.StartPostArchive(ProgrammingChannle);
            ProgrammingChannle.PublishAVideo("The Best Way To Learn Programming", "Why You should learn dsa and problem solving first");
            ProgrammingChannle.PublishAVideo("String Builder", "SomeThing");
            ProgrammingChannle.PublishAPost("How To Work at Google  as Software Enginner", "Mohhmad Moshrif");
            Mortdajabbar.UnSubscribeAVideo(ProgrammingChannle);
            
            ProgrammingChannle.PublishAVideo("Third Video", "Third Content");
            MyArchive.StopVideoArchive(ProgrammingChannle);
            ProgrammingChannle.PublishAVideo("Fourth Video", "Content");
            ProgrammingChannle.PublishAPost("How To Learn Programming in 3 Months", "Mohhmad Moshrif");
            Mortdajabbar.UnSubscribeAPost(ProgrammingChannle);
            ProgrammingChannle.PublishAPost("How To Learn Programming in 6 Months", "Mohhmad Moshrif");
            ProgrammingChannle.PublishAPost("How To Learn Programming in 9 Months", "Mohhmad Moshrif");
            MyArchive.StopPostArchive(ProgrammingChannle);
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("\nEnd\n");
            Console.ReadLine();
        }
    }
}
