import java.util.Random;

public class BingoGrid {
	
	int[][] numGrid ;		// 숫자저장.
	int[][] checkGrid;		// 체크되어진 위치 저장 .
	int[][] priorityGrid;	// 우선순위 저장. 
	int gridSize ;			// 빙고판 사이즈. 
	
	public BingoGrid( int inputGridSize )
	{
		resetAllBingoGrid( inputGridSize );
	}
	
	// 3개의 모든 빙고판 초기화 
	void resetAllBingoGrid( int inputGridSize )
	{
		this.gridSize = inputGridSize; 
		
		numGrid 		= new int[gridSize][gridSize];
		checkGrid 		= new int[gridSize][gridSize];
		priorityGrid 	= new int[gridSize][gridSize];
		
		// 빙고판 모든 값에 0 넣기.
		inputZeroToBingoGrid( numGrid );
		inputZeroToBingoGrid( checkGrid );
		
		// 빙고숫자판에 랜덤값 넣기.
		createRandNumToNumGrid();
		
		resetPriorityGrid();		
	}
	
	//[초기화용] 빙고판 초기화 (모든 숫자를 0으로) 
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
	
	//[초기화용] 빙고판에 중복되지 않는 1~100사이의 숫자들로 채워줌 
	void createRandNumToNumGrid()
	{
		Random rd = new Random() ;	
		int randNumCnt = 0 ;			// 만들어진 빙고 숫자 갯수 저장 
		int randNum = 0;				// 만들어진 난수값 저장.  
		
		// 빙고판 만들기 
		while( randNumCnt < gridSize*gridSize )
		{			
			randNum = Math.abs(((rd.nextInt()) % 100 ) + 1);  // 1~100 사이의 난수 저장.
			
			// 기존 배열의 값들과 입력값이 같은지 확인. 같은 값이 없으면 false반환.  
			if( hasEqualNumOfArr( randNum ) == false )
			{
				enterNumToBingoNumGrid( numGrid, randNum );
				randNumCnt++;				
			}		
		}		
	}
	
	//[초기화용] 숫자 빙고판에 입력된 값을 순서대로 저장.
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
	
	// [초기화용] 빙고판의 위치에 따른 우선순위값 설정.
	// 성공가능한 빙고 라인 갯수에 기초 하여 설정함.
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
	
	// 입력값이 배열에 저장되어 있는 값과 같은지 확인하는 메소드. 존재하면 true반환  
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
	
	// 입력값 위치에 체크 추가
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
	
	// 빙고판1에 해당하는 값이 체크되어 있는지(빙고판2를 보고) 확인하는 함수.
	boolean hasCheckNum( int input )
	{
		for( int i= 0 ; i < gridSize ; i++ )
		{
			for(int j = 0 ; j < gridSize ; j++)
			{
				// 해당값의 빙고판이, 
				if( numGrid[i][j] == input )
				{
					// 체크 되어 있는 상태면 true반환.
					if( checkGrid[i][j] == 1 )					
						return true; 						
					else					
						return false;						
				}
			}			
		}		
		return false;
	}
	
	// 완성된 전체 빙고 라인수 카운트 
	int allSuccBgLine()
	{
		int widthBgCnt = 0;						// 가로 빙고 갯수 카운트 
		int[] heightBgCnt = { 0, 0, 0, 0, 0 };	// 세로 빙고 갯수 카운트 
		int diagLeftUpBgCnt = 0;				// \방향 대각선 빙고 갯수 카운트
		int diagRightUpBgCnt = 0;				// /방향 대각선 빙고 갯수 카운트 
		int allSuccLineCnt = 0;					// 해당 배열의 완성된 빙고라인 갯수 
		
		for( int i = 0 ; i < gridSize ; i++ )
		{
			for(int j = 0 ; j < gridSize ; j++)
			{
				// 빙고판이 체크 되어있을 때, 가로, 세로, 대각선 빙고 갯수 카운트 변수에 저장  
				if( checkGrid[i][j] == 1 )
				{
					widthBgCnt++;
					heightBgCnt[j]++;
					
					// 체크된 빙고의 위치가 대각선(\) 빙고가 가능한 위치일 경우   
					if( i == j )
						diagLeftUpBgCnt++;	
					
					// 체크된 빙고의 위치가 대각선(/) 빙고가 가능한 위치일 경우
					if( i+j == ( gridSize-1 ) )
						diagRightUpBgCnt++;		
					
					// 5번째 줄(맨 아랫줄) 일때만, 
					// 체크된 빙고 위치가 세로, 대각선 위치일 경우 해당 줄의 빙고갯수 확인. 
					if( i == ( gridSize-1 ) )
					{
						// 세로 줄 빙고 라인수 확인 
						if( heightBgCnt[j] == gridSize )			
							allSuccLineCnt++;	
						
						// 대각선 줄 빙고 라인수 확인 / 방향
						if( i+j == ( gridSize-1 ) && diagRightUpBgCnt == gridSize ) 
							allSuccLineCnt++;
						
						// 대각선 줄 빙고 라인수 확인 \ 방향
						if( i+j == ( gridSize-1 )*2 && diagLeftUpBgCnt == gridSize ) 
							allSuccLineCnt++;
					}
				}
			}
			// 가로빙고갯수카운트는 j열(가로열) 끝날때 마다 확인 후, 초기화. 
			if( widthBgCnt == gridSize )
				allSuccLineCnt++;
			widthBgCnt=0;
		}		
		return allSuccLineCnt;		
	}	
	
	// 무작위로 체크되지 않은 빙고판에 체크하고, 해당하는 숫자를 반환 
	int randCheck()
	{ 
		int randNumI = 0;
		int randNumJ = 0;
		int checkNum = 0;	// 체크된 위치에 존재하는 숫자값.  
		Random rd = new Random() ;
		
		while( true )
		{
			randNumI = Math.abs((rd.nextInt()) % gridSize ) ; // 0~4 사이의 난수 
			randNumJ = Math.abs((rd.nextInt()) % gridSize ) ;
			
			// 해당 위치의 빙고 숫자가 체크되지 않은 상태면 
			if(  checkGrid[randNumI][randNumJ] == 0 )
			{
				// 체크된 위치와 같은 위치의 숫자값 저장. 
				checkNum = numGrid[randNumI][randNumJ] ;
								
				// 컴퓨터 빙고판에 체크. 
				this.addCheckToCheckGrid( checkNum );		
				
				break;					
			}
		}		
		return checkNum;
	}	

}
