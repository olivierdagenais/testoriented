namespace StateProblem
{
    public class ExamMarks
    {
        // ReSharper disable InconsistentNaming
        private const int MAX_MARK = 100;
        private static int num_top = 0;
        private static int last_year_top = 1;

        public void has_max(int mark)
        {
            if (mark == MAX_MARK)
                num_top++;
        }

        public int get_num_top()
        { return num_top; }

        public void compare()
        {
            if(get_num_top() > last_year_top)
            {
                /* target branch */
            }
        }
        // ReSharper restore InconsistentNaming
    }
}
