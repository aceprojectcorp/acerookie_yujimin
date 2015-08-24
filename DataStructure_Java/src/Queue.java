class QueueNode
{
	int num ; 
	QueueNode pre;
	QueueNode next;
	
	public QueueNode( int a )
	{
		num = a;
		next = null;
		pre = null;
	}
}

// tail -> [���� �ֱٿ� ���� ���] <-> [���] <->.. <-> [���� ó���� ���� ���] <- head
// PUSH�� ��� tail�������� ��� �߰���.
// POP�� ��� head������ ��尡 ������. (���� ó���� ���� ������ ����)
class QueueList
{
	public void PushToQueue( QueueNode head, QueueNode tail, int input )
	{
		System.out.println("* PUSH : "+input);
		
		QueueNode node = new QueueNode( input );
		
		// ����Ʈ�� ��尡 ����  
		if( tail.next != null )
		{
			node.next = tail.next ; 	// [�� ���] -> [ù��°���] <= tail
			tail.next.pre = node; 		// [�� ���] <- [ù��°���] <= tail 
		}
		else
		{
			head.pre = node;
		}
		tail.next = node;
		
		System.out.println();
		
	}
	
	public void ShowQueue( QueueNode head, QueueNode tail )
	{
		System.out.println("* ť�� ��� ��� ���");
		
		if( tail.next == null )
		{
			System.out.println(" ~ ����Ʈ�� �������");
		}
		else
		{
			QueueNode selectNode = tail.next;
			
			while( selectNode != null )
			{
				System.out.print( selectNode.num + "\t" );
				selectNode = selectNode.next;			
			}			
		}
		
		if( head.pre != null )
		{
			System.out.println("\n - head : "+ head.pre.num);
			System.out.println(" - tail : "+ tail.next.num);
		}
		else
		{
			System.out.println("head : null\ntail : null");
		}
		System.out.println("\n");		
	}
	
	public void PopFromQueue( QueueNode head, QueueNode tail )
	{
		System.out.print("* POP : "  );
		
		if( head.pre == null )
		{
			System.out.println(" ~ ������ ��尡 �����ϴ�.");
		}
		else
		{
			System.out.println( head.pre.num );
			// ���ÿ� ��尡 �� �ϳ� ���� �� 
			if( head.pre.pre == null )
			{
				head.pre = null;
				tail.next = null;
			}
			else
			{
				head.pre = head.pre.pre;
				head.pre.next = null;
			}			
		}
		
		System.out.println();		
	}	
}
public class Queue {

	public static void main(String[] args) {
		
		QueueNode QueueHead = new QueueNode( 0 );
		QueueNode QueueTail = new QueueNode( 0 );
		QueueList Queue = new QueueList();
		
		Queue.PushToQueue( QueueHead, QueueTail, 1 );
		Queue.PushToQueue( QueueHead, QueueTail, 2 );
		Queue.PushToQueue( QueueHead, QueueTail, 3 );
		
		Queue.ShowQueue(QueueHead, QueueTail);
		
		Queue.PopFromQueue(QueueHead, QueueTail);
		
		Queue.ShowQueue(QueueHead, QueueTail);
		
		Queue.PopFromQueue(QueueHead, QueueTail);
		Queue.ShowQueue(QueueHead, QueueTail);
		
		Queue.PopFromQueue(QueueHead, QueueTail);
		Queue.PopFromQueue(QueueHead, QueueTail);
		
		Queue.ShowQueue(QueueHead, QueueTail);
		
		// TODO Auto-generated method stub

	}

}
