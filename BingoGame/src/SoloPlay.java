import java.util.*;
import java.io.IOException;

public class SoloPlay 
{
	public final static int BINGOSIZE = 5 ;
	
	int[][] myBingFan ;		// 내 빙고판 (숫자저장)
	int[][] myBingFan2 ;	// 내 빙고판 (체크저장)	
	int mySuccLineNum;		// 성공한 빙고 라인 수 
	int playTime;			// 플레이 횟수 		
	
	public SoloPlay()
	{
		resetBingo();				
	}		
	
	
	void resetBingo()
	{
		playTime = 0 ;
		mySuccLineNum = 0 ;		
		myBingFan = new int[BINGOSIZE][BINGOSIZE];
		myBingFan2 = new int[BINGOSIZE][BINGOSIZE];
		
		// 빙고판1 초기화   
		resetBingoFan( myBingFan );
		resetBingoFan( myBingFan2 );
		
		// 빙고판 만들기  
		createBingoFan( myBingFan );		
	}
	
	
	// 빙고판 초기화 
	void resetBingoFan( int[][] arr )
	{
		for( int i= 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				arr[i][j] = 0;
			}			
		}		
	}		
	
	
	// 빙고판에 중복되지 않는 1~100사이의 숫자들로 채워줌 
	void createBingoFan( int[][] arr )
	{
		Random rd = new Random() ;	
		int arrCnt = 0 ;			// 만들어진 빙고 숫자 갯수 저장 
		int tmp = 0;				// 만들어진 난수값 저장.  
		
		// 빙고판 만들기 
		while( arrCnt < BINGOSIZE*BINGOSIZE )
		{			
			tmp = Math.abs(((rd.nextInt()) % 100 ) + 1);  // 1~100 사이의 난수 저장.
			
			// 기존 배열의 값들과 입력값이 같은지 확인. 같은 값이 없으면 false반환.  
			if( hasEqualNumOfArr( arr, tmp ) == false )
			{
				initArr( arr, tmp );
				arrCnt++;				
			}		
		}		
	}
	
	
	// 입력값이 배열에 저장되어 있는 값과 같은지 확인하는 메소드. 존재하면 true반환  
	boolean hasEqualNumOfArr( int[][] arr, int input )
	{
		for( int i= 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				if( arr[i][j] == input )
					return true;				
			}			
		}	
		return false;
	}
	
	
	// 빙고판(myBingFan) [초기화용]. 입력된 값을 순서대로 저장.
	void initArr( int[][] arr, int num )
	{
		for( int i= 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				if( arr[i][j] == 0 )
				{
					arr[i][j] = num;
					return;
				}				
			}			
		}		
	}
	
	
	// 빙고판 출력 - 입력된 빙고 번호의 체크까지 함께 출력 
	void printBingoFan()
	{		
		System.out.println("\n\n============ << 내 빙고판 >> ==============");
		for( int i= 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				if( myBingFan2[i][j] == 1 )
				{
					System.out.print( "(" + myBingFan[i][j] + ")\t" );
				}
				else
				{
					System.out.print( myBingFan[i][j] +"\t" );
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
			
			printBingoFan();
			
			if( playTime >= 2 )
			{
				System.out.println("* 완성된 빙고 라인수 : " + mySuccLineNum ) ;
			}		
			
			checkBingoFan2( isValidNum() );	
			mySuccLineNum = checkSuccLine( myBingFan2 );  
		}		
		
		printBingoFan();
		System.out.println("\n\n!!! --- 승 리 --- !!!\n\n");
	}
	
	
	// 입력값(선택할 빙고 번호)이 유효한 값인지 확인하고 유효한 값이면 반환해주는 메소드.
	int isValidNum()
	{
		int num = 0 ; 
		System.out.print("* 선택할 번호 : ");		 		
		Scanner sc = new Scanner( System.in );
		
		while( true )
		{					
			// 입력값 자체가 잘못 되었을 경우 ( 문자, 특문, 실수등이 입력될 경우)
			if( !sc.hasNextInt() )			// 여기서 실질적으로 입력 받음 
			{		
				sc.nextLine();				// 잘못된 입력값을 비움. _ hasNext에 남아있던 개행문자를 이용해서 스캐너 비움. 				
				System.err.println(" ~ 숫자를 입력해 주세요.");
				continue;
			}
			// 입력값이 일단 숫자. 
			else
			{
				// 숫자+공백+숫자로 이루어졌는지 확인 -------------------
				String tmpStr = sc.nextLine();
				int tmp = 0 ;
				// (공백찾기) 
				for( int i = 0 ; i < tmpStr.length() ; i ++ )
				{
					if( tmpStr.charAt(i) == ' ')
					{						
						tmp++;
						break;
					}					
				}
				if( tmp > 0 )
				{
					System.err.println(" ~ 공백없이 입력해 주세요. ");
					continue;
				}
				//-----------------------------------------------
				
				num = Integer.parseInt( tmpStr );
			}
			
			// 값의 범위 
			if( num < 1 || num > 100 )
			{
				System.err.println(" ~ 1~100사이의 숫자를 입력해주세요. ");
				continue;
				
			}			
			// 빙고판에 있는 값이면 false를 내보냄
			else if( hasEqualNumOfArr( myBingFan, num ) == false )
			{
				System.err.println(" ~ 빙고판에 없는 숫자입니다. 다시입력하세요. ");
				continue;
				
			}
			// 빙고판에 체크된 숫자인지 확인 
			else if( checkBingoFan1( myBingFan, myBingFan2, num ) == true )			
				System.err.println(" ~ 이미 선택되어진 숫자입니다. 다른숫자를 선택해 주세요.");
			
			else
			{				
				break;
			}		
		}
		
		return num;			
	}
	
	
	// 빙고판1에 체크된 값을 빙고판2에 저장.
	void checkBingoFan2( int input )
	{
		for( int i= 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				if( myBingFan[i][j] == input )
				{
					myBingFan2[i][j] = 1;
					return;
				}
			}			
		}		
	}
	
	
	// 빙고판1에 해당하는 값이 체크되어 있는지(빙고판2를 보고) 확인하는 함수.
	boolean checkBingoFan1( int[][] arr, int[][] arr2, int input )
	{
		for( int i= 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				// 해당값의 빙고판이, 
				if( arr[i][j] == input )
				{
					// 체크 되어 있는 상태면 true반환.
					if( arr2[i][j] == 1 )					
						return true; 						
					else					
						return false;						
				}
			}			
		}		
		return false;
	}
	
	
	// 완성된 빙고 라인수 카운트 
	int checkSuccLine( int[][] arr )
	{
		int x = 0;						// 가로 빙고 갯수 카운트 
		int[] y = { 0, 0, 0, 0, 0 };	// 세로 빙고 갯수 카운트 
		int[] z = { 0, 0 };				// 대각선 빙고 갯수 카운트 . 앞에꺼 \방향, 뒤에꺼 / 방향 
		int tmpBingoNum = 0;
		
		for( int i = 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				// 빙고판이 체크 되어있을 때,   
				if( arr[i][j] == 1 )
				{
					// 가로, 세로, 대각선 빙고 갯수 카운트 변수에 저장
					x++;
					y[j]++;
					
					if( i == j )
						z[0]++;	
					
					if( i+j == (BINGOSIZE-1) )
						z[1]++;		
					
					// 5번째 줄(맨 아랫줄) 일때만 돌음.
					if( i == (BINGOSIZE-1) )
					{
						// 세로 줄 빙고 라인수 확인 
						if( y[j] == BINGOSIZE )			
							tmpBingoNum++;	
						
						// 대각선 줄 빙고 라인수 확인 / 방
						if( i+j == (BINGOSIZE-1) && z[1] == BINGOSIZE ) 
							tmpBingoNum++;
						
						if( i+j == (BINGOSIZE-1)*2 && z[0] == BINGOSIZE ) 
							tmpBingoNum++;
					}
				}
			}
			
			if( x == BINGOSIZE )
				tmpBingoNum++;
			x=0;
		}		
		return tmpBingoNum;		
	}	
	
}
