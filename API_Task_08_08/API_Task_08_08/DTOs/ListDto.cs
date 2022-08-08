using System.Collections.Generic;

namespace API_Task_08_08.DTOs
{
    public class ListDto<T>
    {
        public List<T> ListItemDtos { get; set; }
        public int TotalCount { get; set; }
    }
}
