namespace JsModulesDemo.Model
{
    public record UserModel {
        public Guid Id { get; init; }
        public string Name { get; init; }

        public UserModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
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
        public string Text { get; init; }
        public QuestionModel(Guid id, UserModel author, string text)
        {
            Id = id;
            Author = author;
            Text = text;
        }
    }
}
