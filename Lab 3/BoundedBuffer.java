import java.util.concurrent.Semaphore;
import java.util.Date;

class Producer implements Runnable{
	private Buffer buffer;
	
	public Producer(Buffer buffer) {
		this.buffer = buffer;
	}
	
	public void run() {
		Date message;
		
		while(true) {
			//nap for a while
			SleepUtilities.nap();
			
			//produce an item & enter it into the buffer
			message = new Date();
			buffer.insert(message);
			System.out.println("Producer produced " + message);
		}
	}
}

class Consumer implements Runnable{
	private Buffer buffer;
	
	public Consumer(Buffer buffer) {
		this.buffer = buffer;
	}
	
	public void run() {
		Date message;
		
		while(true) {
			//nap for a while
			SleepUtilities.nap();
			
			//consume an item from the buffer
			message = (Date)buffer.remove();
			System.out.println("Consumer consumed " + message );
		}
	}
}

public class BoundedBuffer implements Buffer {
	private static final int BUFFER_SIZE = 5; //buffer size
	private Object[] buffer; // the array of object 
	private int in, out; // position for first in and first out
	private Semaphore mutex; // limit the access
	private Semaphore empty; // number of empty element
	private Semaphore full; // number of filled element
	
	public BoundedBuffer() {
		// buffer is initially empty
		in = 0;
		out = 0;
		mutex = new Semaphore(1);
		empty = new Semaphore(BUFFER_SIZE); 
		full = new Semaphore(0); // start with no element
		
		buffer = new Object[BUFFER_SIZE];
	}
	
	//Producers call this method
	public void insert(Object item) {
		try {
			empty.acquire();
			mutex.acquire();
		}
		catch(InterruptedException e) {
			System.out.println("Error: " + e);
		}
		// add an item to the buffer
		buffer[in] = item;
		in = (in + 1) % BUFFER_SIZE;
		
		mutex.release();
		full.release();
		
	}
	
	//Consumers call this method
	public Object remove() {
		Object item;
		try {
			full.acquire();
			mutex.acquire();
		
		}catch(InterruptedException e) {
			System.out.println("Error: " + e);
		}			
		//remove an item from the buffer
		item = buffer[out];
		out = (out + 1) % BUFFER_SIZE;
		mutex.release();
		empty.release();
		return item;
		
	}
	public static void main(String[] args) {
		Buffer buffer = new BoundedBuffer();
		Thread producer = new Thread(new Producer(buffer));
		Thread consumer = new Thread(new Consumer(buffer));
		
		producer.start();
		consumer.start();
	}
}