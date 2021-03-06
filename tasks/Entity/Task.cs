using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tasks.Entity
{
    public class Task
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Title { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [MaxLength(1000)]
        public string Tags { get; set; }

        public DateTimeOffset OnADay { get; set; }

        public DateTimeOffset AtATime { get; set; }

        public ETaskStatus Status { get; set; }

        public ETaskRepeat Repeat { get; set; }

        public ETaskPriority Priority { get; set; }

        public string Location { get; set; }

        public string Url { get; set; }
        [Obsolete("Only for entity binding")]
        public Task() { }

        public Task(string title, string description="", string tags="", DateTimeOffset onADay=default(DateTimeOffset),
            DateTimeOffset atATime = default(DateTimeOffset), ETaskStatus status = ETaskStatus.None, 
            ETaskRepeat repeat = ETaskRepeat.Never, ETaskPriority priority = ETaskPriority.None, string location="", string url="")
        {
            this.Id = Guid.NewGuid();
            this.Title = title;
            this.Description = description;
            this.Tags = tags;
            this.OnADay = onADay;
            this.AtATime = atATime;
            this.Status = status;
            this.Repeat = repeat;
            this.Priority = priority;
            this.Location = location;
            this.Url = url;

        }
    }
}