/* Part 1
 * Figures 4.13, 4.14, 4.15 */

//import java.util.Date;
//
//// Producer thread
//class Producer implements Runnable{
//		private Channel<Date> queue;
//		
//		public Producer(Channel<Date> queue) {
//			this.queue = queue;
//		}
//		public void run() {
//			Date message;
//			
//			while(true) {
//				//nap for a while
//				SleepUtilities.nap();
//				
//				//produce an item and enter it into the buffer
//				message = new Date();
//				System.out.println("Producer produced " + message);
//				queue.send(message);
//			}
//		}
//	}
//	
////Consumer Thread
//class Consumer implements Runnable{
//	private Channel<Date> queue;
//	
//	public Consumer(Channel<Date> queue) {
//		this.queue = queue;
//	}
//	
//	public void run() {
//		Date message;
//		
//		while(true) {
//			// nap for a while
//			SleepUtilities.nap();
//			
//			// Consume an item from the buffer
//			message = queue.receive();
//			
//			//Make sure consumer receives something from the buffer and print
//			if(message != null) {
//				System.out.println("Consumer consumed " + message);
//			}
//		}
//	}
//}
//
//public class multithread {
//	
//	public static void main(String[] args) {
//			// create the message queue
//			Channel<Date> queue = new MessageQueue<Date>();
//			
//			// Create the producer and consumer threads and pass
//			// each thread a reference to the MessageQueue object
//			Thread producer = new Thread(new Producer(queue));
//			Thread consumer = new Thread(new Consumer(queue));
//			
//			// start the threads
//			producer.start();
//			consumer.start();
//		}
//}
//
