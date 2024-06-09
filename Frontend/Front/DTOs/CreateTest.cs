using System.ComponentModel.DataAnnotations;

namespace Front.DTOs
{
        public class CreateTest
        {
        [Required(ErrorMessage ="*Обязательно для заполнения")]
        [MaxLength(100, ErrorMessage = "Не больше 100 символов")]
        [MinLength(5, ErrorMessage = "Не меньше 5 символов")]
            public string title { get; set; }
        [Required(ErrorMessage = "*Обязательно для заполнения")]
        [MaxLength(300, ErrorMessage = "Не больше 300 символов")]
        [MinLength(5, ErrorMessage = "Не меньше 5 символов")]
        public string description { get; set; }
        [Required(ErrorMessage = "*Обязательно для заполнения")]
        [MaxLength(100, ErrorMessage = "Не больше 100 символов")]
        [MinLength(5, ErrorMessage = "Не меньше 5 символов")]
        public string category { get; set; } = String.Empty;
        [Required(ErrorMessage = "*Обязательно для заполнения")]

        public int level { get; set; } = 0;
        [Required(ErrorMessage = "*Обязательно для заполнения")]
        public bool isPrivate { get; set; }
        public string companyOwners { get; set; } = String.Empty;
        [ValidateComplexType]
        public List<CreateQuestion> questions { get; set; } = new List<CreateQuestion>();
        }

        public class CreateQuestion
        {
        [Required(ErrorMessage = "*Обязательно для заполнения")]
        [MaxLength(300, ErrorMessage = "Не больше 300 символов")]
        [MinLength(5, ErrorMessage = "Не меньше 5 символов")]
        public string questionTitle { get; set; }
            public int optionCount { get; set; }
            public int correctOptionCount { get; set; }
        [ValidateComplexType]
        public CreateQuestioncorrectinfo questionCorrectInfo { get; set; } = new CreateQuestioncorrectinfo();
        [ValidateComplexType]
        public List<Createquestionoption> createQuestionOptions { get; set; } = new List<Createquestionoption>();
        }

        public class CreateQuestioncorrectinfo
        {

        [Required(ErrorMessage = "*Обязательно для заполнения")]
        [MaxLength(150, ErrorMessage = "Не больше 150 символов")]
        [MinLength(1, ErrorMessage = "Не меньше 1 символов")]
        public string title { get; set; }
        public string imageLink { get; set; } = String.Empty;
        public string videoLink { get; set; } = String.Empty;
        }

        public class Createquestionoption
        {

        [Required(ErrorMessage = "*Обязательно для заполнения")]
        [MaxLength(100, ErrorMessage = "Не больше 100 символов")]
        [MinLength(5, ErrorMessage = "Не меньше 5 символов")]
        public string text { get; set; }
            public bool isCorrect { get; set; }
        }

    }

