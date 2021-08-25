public class Program {
    public static void main(String[] args)
    {
        try
        {
            Labirinto lab = new Labirinto(10,"quebrado");
            System.out.println(lab);
        }
        catch (Exception e)
        {
            System.err.println(e.getMessage());
        }
    }

}
