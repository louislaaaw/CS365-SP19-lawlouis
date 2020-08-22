public class SleepUtilities
{ 
  public static void nap()      // Nap between zero and NAP_TIME seconds
  {
    nap(NAP_TIME);
  }

  public static void nap(int duration) 
  {
    int sleeptime = (int) (NAP_TIME * Math.random() );
    try 
    { 
      Thread.sleep(sleeptime*1000); 
    }
    catch (InterruptedException e) {}
  }
  
  private static final int NAP_TIME = 5;
}