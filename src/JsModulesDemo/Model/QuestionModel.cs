namespace JsModulesDemo.Model
{
    public record UserModel {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
    }

    public record LikeModel {
        public UserModel User { get; init; }

        public LikeModel(UserModel user)
        {
            User = user;
        }
    }

    public record QuestionModel
    {
        public Guid Id { get; init; }
        public List<LikeModel> Likes { get; init; } = new List<LikeModel>();
        public UserModel Author { get; init; }
        public DateTime Created { get; init; }
        public string Text { get; init; }
        public QuestionModel(Guid id, UserModel author, string text, DateTime created)
        {
            Id = id;
            Author = author;
            Text = text;
            Created = created;
        }

        public void ToggleLike(UserModel user)
        {
            var like = Likes.FirstOrDefault(l => l.User.Id == user.Id);
            if (like is not null)
            {
                Likes.Remove(like);
            }
            else
            {
                Likes.Add(new LikeModel(user with { }));
            }
        }
    }
}
