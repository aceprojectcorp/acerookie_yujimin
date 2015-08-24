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

// tail -> [가장 최근에 들어온 노드] <-> [노드] <->.. <-> [제일 처음에 들어온 노드] <- head
// PUSH할 경우 tail방향으로 노드 추가됨.
// POP할 경우 head방향의 노드가 삭제됨. (제일 처음에 들어온 노드부터 삭제)
class QueueList
{
	public void PushToQueue( QueueNode head, QueueNode tail, int input )
	{
		System.out.println("* PUSH : "+input);
		
		QueueNode node = new QueueNode( input );
		
		// 리스트에 노드가 존재  
		if( tail.next != null )
		{
			node.next = tail.next ; 	// [새 노드] -> [첫번째노드] <= tail
			tail.next.pre = node; 		// [새 노드] <- [첫번째노드] <= tail 
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
		System.out.println("* 큐의 모든 노드 출력");
		
		if( tail.next == null )
		{
			System.out.println(" ~ 리스트가 비어있음");
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
			System.out.println(" ~ 삭제할 노드가 없습니다.");
		}
		else
		{
			System.out.println( head.pre.num );
			// 스택에 노드가 단 하나 뿐일 때 
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
