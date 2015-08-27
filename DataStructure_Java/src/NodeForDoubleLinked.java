
public class NodeForDoubleLinked {
	
	int num ; 
	NodeForDoubleLinked pre;
	NodeForDoubleLinked next;
	
	public NodeForDoubleLinked( int a )
	{
		num = a;
		pre = null;
		next = null;
	}
	
	public NodeForDoubleLinked( )
	{
		num = 0;
		pre = null;
		next = null;
	}
}
