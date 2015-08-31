public class SoloPlay 
{
	public final static int BINGOSIZE = 5 ;
	
	BingoGrid myBG ;		// 내 빙고판 객체 
	int mySuccLineNum;		// 성공한 빙고 라인 수 
	int playTime;			// 플레이 횟수 	
	
	
	public SoloPlay()
	{
		resetBingo();				
	}		
	
	
	void resetBingo()
	{
		myBG = new BingoGrid( BINGOSIZE );
		playTime = 0 ;
		mySuccLineNum = 0 ;			
	}
		
	// 빙고판 출력 - 입력된 빙고 번호의 체크까지 함께 출력 
	void printBG()
	{		
		System.out.println("\n\n============ << 내 빙고판 >> ==============");
		for( int i= 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				if( myBG.checkGrid[i][j] == 1 )
				{
					System.out.print( "(" + myBG.numGrid[i][j] + ")\t" );
				}
				else
				{
					System.out.print( myBG.numGrid[i][j] +"\t" );
				}
				
			}	System.out.println();		
		}
		System.out.println("========================================");		
	}
	
	// 실질적 게임 플레이 반복 함수. 완성된 빙고라인수가 4개 이하인 경우 계속 반복됨. 
	void play()
	{		
		while( mySuccLineNum < 5 ) // true )
		{
			++playTime;
			
			printBG();
			
			if( playTime >= 2 )
			{
				System.out.println("* 완성된 빙고 라인수 : " + mySuccLineNum ) ;
			}		
			
			// 입력값에 해당하는 숫자 체크 
			myBG.addCheckToCheckGrid( isValidNum( myBG ) );
			
			// 내 완성된 전체 빙고라인 수 저장. 
			mySuccLineNum = myBG.allSuccBgLine(); 
		}		
		
		printBG();
		System.out.println("\n\n!!! --- 승 리 --- !!!\n\n");
	}
	
	
	// 입력값(선택할 빙고 번호)이 유효한 값인지 확인하고 유효한 값이면 반환해주는 메소드.
	int isValidNum( BingoGrid bg )
	{
		int num = 0 ; 
		System.out.print("* 선택할 번호 : ");		 		
		
		while( true )
		{	
			num = playMain.inputNum() ; // 입력값이 숫자만으로 이루어졌을 때 int값 반환. 
			
			// 값의 범위 
			if( num < 1 || num > 100 )
			{
				System.err.println(" ~ 1~100사이의 숫자를 입력해주세요. ");
				continue;				
			}			
			// 빙고판에 존재하는 값인지 확인
			else if( myBG.hasEqualNumOfArr( num ) == false )
			{
				System.err.println(" ~ 빙고판에 없는 숫자입니다. 다시입력하세요. ");
				continue;				
			}
			// 체크된 숫자인지 확인 
			else if( bg.hasCheckNum( num ) == true )
				System.err.println(" ~ 이미 선택되어진 숫자입니다. 다른숫자를 선택해 주세요.");			
			else
			{				
				break;
			}		
		}		
		return num;			
	}	
	
}
