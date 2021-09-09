namespace Puzzle15
{
    interface Interface
    {
        int count { get; set; }
        void start();
        int get_num(int pos);
        void shift(int pos);
        void ran();
        bool check();
    }
}
