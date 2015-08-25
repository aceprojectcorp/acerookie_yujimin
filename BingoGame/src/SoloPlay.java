import java.util.*;
import java.io.IOException;
import java.lang.*;


class Bingo
{
	public final static int BINGOSIZE = 5 ;
	
	int num ; 				// 난수를 만들어서 저장. 배열에 저장하기 유효한 값인지 확인.
	int[][] myBingFan ;		// 내 빙고판 (숫자저장)
	int[][] myBingFan2 ;	// 내 빙고판 (체크저장)	
	int succLineNum;		// 성공한 빙고 라인 수 
	int playTime;			// 플레이 횟수 
	Random rd ;				// 난수값 생성 
	Scanner sc ;			// 입력값 받아줄 스캐너.
	
	
	public Bingo()
	{
		resetBingo();				
	}	
	
	
	void resetBingo()
	{
		int arrCnt = 0 ;			// 만들어진 빙고 숫자 갯수 저장 
		playTime = 0 ;
		succLineNum = 0 ;
		num = 0 ; 
		myBingFan = new int[BINGOSIZE][BINGOSIZE];
		myBingFan2 = new int[BINGOSIZE][BINGOSIZE];
		rd = new Random() ;
		sc = new Scanner( System.in );
		
		// 빙고판1,2 초기화  
		for( int i= 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				myBingFan[i][j] = 0;
				myBingFan2[i][j] = 0;
			}			
		}
		
		// 빙고판 만들기 
		while( arrCnt < BINGOSIZE*BINGOSIZE )
		{			
			num = ((rd.nextInt()) % 100 ) + 1;  // 1~100 사이의 난수 저장.
			
			if( num < 0 )
				num *= -1 ;
			
			// 기존 배열의 값들과 입력값이 같은지 확인. 같은 값이 없으면 false반환.  
			if( isEqualNumOfArr( num ) == false )
			{
				initArr(myBingFan, num);
				arrCnt++;				
			}		
		}
		
		num = 0 ;
	}
	
	
	// num값이 배열에 저장되어 있는 값과 같은지 확인하는 메소드 
	boolean isEqualNumOfArr( int input )
	{
		for( int i= 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				if( myBingFan[i][j] == input )
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
		while( succLineNum < 5 ) // true )
		{
			++playTime;
			
			printBingoFan();
			
			if( playTime >= 2 )
			{
				System.out.println("* 완성된 빙고 라인수 : " + succLineNum ) ;
			}		
			
			checkBingoFan2( isValidNum() );	
			succLineNum = checkSuccLine();  
		}		
		
		printBingoFan();
		System.out.println("\n\n!!! --- 승 리 --- !!!\n\n");
	}
	
	
	// 입력값이 유효한 값인지 확인하는 메소드.
	int isValidNum()
	{

		System.out.print("* 선택할 번호 : ");		 
		
		while( true )
		{			
			// 입력값 자체가 잘못 되었을 경우 ( 문자, 특문, 실수 )
			if( !sc.hasNextInt() )
			{		
				sc.next();
				System.err.println(" ~ 숫자를 입력해 주세요.");
				continue;
			}
			else
			{
				num = sc.nextInt();			
			}
			
			// 값의 범위 
			if( num < 1 || num > 100 )
			{
				System.err.println(" ~ 1~100사이의 숫자를 입력해주세요. ");
				continue;
				
			}			
			// 빙고판에 있는 값이면 false를 내보냄
			else if( isEqualNumOfArr( num ) == false )
			{
				System.err.println(" ~ 빙고판에 없는 숫자입니다. 다시입력하세요. ");
				continue;
				
			}
			// 빙고판에 체크된 숫자인지 확인 
			else if( checkBingoFan1( num ) == true )
			{
				System.out.println(" ~ 이미 선택되어진 숫자입니다. 다른숫자를 선택해 주세요.");
			}
			else
			{
				break;
			}		
		}
		
		return num;	
		
		// 입력값 자체가 잘못되었을경우 ( 문자....특수문자... )
		// 숫자가 아닌 문자열등을 잘못 입력했을 때, 올바른 값을 입력할 때까지 루프를 돌리며 계속 다시 입력받음
		// http://mwultong.blogspot.com/2007/03/java-input-float-number-loop.html
//		while( !sc.hasNextInt() )
//		{
//			sc.next();	// 잘못된 입력값 버리기.
//			System.err.println(" ~ 숫자를 입력해 주세요.");	
//		}
		
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
	boolean checkBingoFan1( int input )
	{
		for( int i= 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				// 해당값의 빙고판이, 
				if( myBingFan[i][j] == input )
				{
					// 체크 되어 있는 상태면 true반환.
					if( myBingFan2[i][j] == 1 )					
					{	return true; 	}					
					else					
					{	return false;	}					
				}
			}			
		}
		
		return false;
	}
	
	int checkSuccLine()
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
				if( myBingFan2[i][j] == 1 )
				{
					// 가로, 세로, 대각선 빙고 갯수 카운트 변수에 저장
					x++;
					y[j]++;
					
					if( i == j )
						z[0]++;	
					
					if( i+j == 4 )
						z[1]++;		
					
					// 5번째 줄(맨 아랫줄) 일때만 돌음.
					if( i == 4 )
					{
						// 세로 줄 빙고 라인수 확인 
						if( y[j] == 5 )			
							tmpBingoNum++;	
						
						// 대각선 줄 빙고 라인수 확인 / 방
						if( i+j == 4 && z[1] == 5 ) 
							tmpBingoNum++;
						
						if( i+j == 8 && z[0] == 5 ) 
							tmpBingoNum++;
					}
				}
			}
			
			if( x == 5 )
				tmpBingoNum++;
			x=0;
		}
		
		return tmpBingoNum;		
	}
	

	
	
}


public class SoloPlay {
		
	public static void main(String[] args) throws IOException {
		
		Bingo bingo = new Bingo();
		String inputSt = null;
		
		char inputChar;		// 입력값 받기 
		
		bingo.play();
		
		// 승리 후
		while( true )
		{ 
			System.out.print("\n\n* 다시 게임을 시작하시겠습니까? (y/n) : ");
			
			while( true )
			{
				Scanner sc = new Scanner(System.in);
				
				inputSt = sc.nextLine();

				if( inputSt.compareTo("y") == 0 ||
					inputSt.compareTo("Y") == 0 ||
					inputSt.compareTo("n") == 0 ||
					inputSt.compareTo("N") == 0
				 )				
				{
					break;
				}
				else
				{				
					System.err.println(" ~ y 또는 n 값만 입력해 주세요.");
				}
			}
			
			if( inputSt.compareTo("n") == 0 || inputSt.compareTo("N") == 0  )
			{
				System.out.println(" ~ 게임종료 ");
				break;
			}
			else if( inputSt.compareTo("y") == 0 || inputSt.compareTo("Y") == 0 )
			{
				bingo.resetBingo();
				bingo.play();
			}			
		}		
	}
}
