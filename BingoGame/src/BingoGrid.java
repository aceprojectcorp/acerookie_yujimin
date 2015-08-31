import java.util.Random;

public class BingoGrid {
	
	int[][] numGrid ;		// ��������.
	int[][] checkGrid;		// üũ�Ǿ��� ��ġ ���� .
	int[][] priorityGrid;	// �켱���� ����. 
	int gridSize ;			// ������ ������. 
	
	public BingoGrid( int inputGridSize )
	{
		resetAllBingoGrid( inputGridSize );
	}
	
	// 3���� ��� ������ �ʱ�ȭ 
	void resetAllBingoGrid( int inputGridSize )
	{
		this.gridSize = inputGridSize; 
		
		numGrid 		= new int[gridSize][gridSize];
		checkGrid 		= new int[gridSize][gridSize];
		priorityGrid 	= new int[gridSize][gridSize];
		
		// ������ ��� ���� 0 �ֱ�.
		inputZeroToBingoGrid( numGrid );
		inputZeroToBingoGrid( checkGrid );
		
		// ���������ǿ� ������ �ֱ�.
		createRandNumToNumGrid();
		
		resetPriorityGrid();		
	}
	
	//[�ʱ�ȭ��] ������ �ʱ�ȭ (��� ���ڸ� 0����) 
	void inputZeroToBingoGrid( int[][] arr )
	{
		for( int i= 0 ; i < gridSize ; i++ )
		{
			for(int j = 0 ; j < gridSize ; j++)
			{
				arr[i][j] = 0;
			}			
		}
	}
	
	//[�ʱ�ȭ��] �����ǿ� �ߺ����� �ʴ� 1~100������ ���ڵ�� ä���� 
	void createRandNumToNumGrid()
	{
		Random rd = new Random() ;	
		int randNumCnt = 0 ;			// ������� ���� ���� ���� ���� 
		int randNum = 0;				// ������� ������ ����.  
		
		// ������ ����� 
		while( randNumCnt < gridSize*gridSize )
		{			
			randNum = Math.abs(((rd.nextInt()) % 100 ) + 1);  // 1~100 ������ ���� ����.
			
			// ���� �迭�� ����� �Է°��� ������ Ȯ��. ���� ���� ������ false��ȯ.  
			if( hasEqualNumOfArr( randNum ) == false )
			{
				enterNumToBingoNumGrid( numGrid, randNum );
				randNumCnt++;				
			}		
		}		
	}
	
	//[�ʱ�ȭ��] ���� �����ǿ� �Էµ� ���� ������� ����.
	void enterNumToBingoNumGrid( int[][] arr, int num )
	{
		for( int i= 0 ; i < gridSize ; i++ )
		{
			for(int j = 0 ; j < gridSize ; j++)
			{
				if( arr[i][j] == 0 )
				{
					arr[i][j] = num;
					return;
				}				
			}			
		}		
	}
	
	// [�ʱ�ȭ��] �������� ��ġ�� ���� �켱������ ����.
	// ���������� ���� ���� ������ ���� �Ͽ� ������.
	void resetPriorityGrid()
	{
		if( gridSize == 5 )
		{
			int tmpArr[][]
				= { {3, 2, 2, 2, 3},
					{2, 3, 2, 3, 2},
					{2, 2, 4, 2, 2},
					{2, 3, 2, 3, 2},
					{3, 2, 2, 2, 3}
					};	

			for( int i= 0 ; i < gridSize ; i++ )
			{
				for(int j = 0 ; j < gridSize ; j++)
				{
					priorityGrid[i][j] = tmpArr[i][j];
				}			
			}
		}

		else if( gridSize == 3 )
		{
			int tmpArr2[][]
					= { {3, 2, 3},
						{2, 4, 2},
						{3, 2, 3},
					  };	
				for( int i= 0 ; i < gridSize ; i++ )
				{
					for(int j = 0 ; j < gridSize ; j++)
					{
						priorityGrid[i][j] = tmpArr2[i][j];
					}
				}
		}		
	}
	
	// �Է°��� �迭�� ����Ǿ� �ִ� ���� ������ Ȯ���ϴ� �޼ҵ�. �����ϸ� true��ȯ  
	boolean hasEqualNumOfArr( int input )
	{
		for( int i= 0 ; i < gridSize ; i++ )
		{
			for(int j = 0 ; j < gridSize ; j++)
			{
				if( numGrid[i][j] == input )
					return true;				
			}			
		}	
		return false;
	}
	
	// �Է°� ��ġ�� üũ �߰�
	public void addCheckToCheckGrid( int input )
	{
		for( int i= 0 ; i < gridSize ; i++ )
		{
			for(int j = 0 ; j < gridSize ; j++)
			{
				if( numGrid[i][j] == input )
				{
					checkGrid[i][j] = 1;
					return;
				}
			}			
		}		
	}
	
	// ������1�� �ش��ϴ� ���� üũ�Ǿ� �ִ���(������2�� ����) Ȯ���ϴ� �Լ�.
	boolean hasCheckNum( int input )
	{
		for( int i= 0 ; i < gridSize ; i++ )
		{
			for(int j = 0 ; j < gridSize ; j++)
			{
				// �ش簪�� ��������, 
				if( numGrid[i][j] == input )
				{
					// üũ �Ǿ� �ִ� ���¸� true��ȯ.
					if( checkGrid[i][j] == 1 )					
						return true; 						
					else					
						return false;						
				}
			}			
		}		
		return false;
	}
	
	// �ϼ��� ��ü ���� ���μ� ī��Ʈ 
	int allSuccBgLine()
	{
		int widthBgCnt = 0;						// ���� ���� ���� ī��Ʈ 
		int[] heightBgCnt = { 0, 0, 0, 0, 0 };	// ���� ���� ���� ī��Ʈ 
		int diagLeftUpBgCnt = 0;				// \���� �밢�� ���� ���� ī��Ʈ
		int diagRightUpBgCnt = 0;				// /���� �밢�� ���� ���� ī��Ʈ 
		int allSuccLineCnt = 0;					// �ش� �迭�� �ϼ��� �������� ���� 
		
		for( int i = 0 ; i < gridSize ; i++ )
		{
			for(int j = 0 ; j < gridSize ; j++)
			{
				// �������� üũ �Ǿ����� ��, ����, ����, �밢�� ���� ���� ī��Ʈ ������ ����  
				if( checkGrid[i][j] == 1 )
				{
					widthBgCnt++;
					heightBgCnt[j]++;
					
					// üũ�� ������ ��ġ�� �밢��(\) ������ ������ ��ġ�� ���   
					if( i == j )
						diagLeftUpBgCnt++;	
					
					// üũ�� ������ ��ġ�� �밢��(/) ������ ������ ��ġ�� ���
					if( i+j == ( gridSize-1 ) )
						diagRightUpBgCnt++;		
					
					// 5��° ��(�� �Ʒ���) �϶���, 
					// üũ�� ���� ��ġ�� ����, �밢�� ��ġ�� ��� �ش� ���� �������� Ȯ��. 
					if( i == ( gridSize-1 ) )
					{
						// ���� �� ���� ���μ� Ȯ�� 
						if( heightBgCnt[j] == gridSize )			
							allSuccLineCnt++;	
						
						// �밢�� �� ���� ���μ� Ȯ�� / ����
						if( i+j == ( gridSize-1 ) && diagRightUpBgCnt == gridSize ) 
							allSuccLineCnt++;
						
						// �밢�� �� ���� ���μ� Ȯ�� \ ����
						if( i+j == ( gridSize-1 )*2 && diagLeftUpBgCnt == gridSize ) 
							allSuccLineCnt++;
					}
				}
			}
			// ���κ�������ī��Ʈ�� j��(���ο�) ������ ���� Ȯ�� ��, �ʱ�ȭ. 
			if( widthBgCnt == gridSize )
				allSuccLineCnt++;
			widthBgCnt=0;
		}		
		return allSuccLineCnt;		
	}	
	
	// �������� üũ���� ���� �����ǿ� üũ�ϰ�, �ش��ϴ� ���ڸ� ��ȯ 
	int randCheck()
	{ 
		int randNumI = 0;
		int randNumJ = 0;
		int checkNum = 0;	// üũ�� ��ġ�� �����ϴ� ���ڰ�.  
		Random rd = new Random() ;
		
		while( true )
		{
			randNumI = Math.abs((rd.nextInt()) % gridSize ) ; // 0~4 ������ ���� 
			randNumJ = Math.abs((rd.nextInt()) % gridSize ) ;
			
			// �ش� ��ġ�� ���� ���ڰ� üũ���� ���� ���¸� 
			if(  checkGrid[randNumI][randNumJ] == 0 )
			{
				// üũ�� ��ġ�� ���� ��ġ�� ���ڰ� ����. 
				checkNum = numGrid[randNumI][randNumJ] ;
								
				// ��ǻ�� �����ǿ� üũ.
				this.addCheckToCheckGrid( checkNum );		
				
				break;					
			}
		}		
		return checkNum;
	}	

}