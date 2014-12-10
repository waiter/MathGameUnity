using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map {

	private const int SIZE=9;

	private Cell[][] _numbers=new Cell[SIZE][];//[row,col]

	public Map(){
		Initilize();
	}

	private void Initilize(){
		for(int i=0;i<SIZE;i++){
			_numbers[i]=new Cell[SIZE];
			for(int j=0;j<SIZE;j++){
				_numbers[i][j]=new Cell((i*3+i/3+j)%SIZE+1,i,j);
			}
		}
	}

	//verticle
	private void SwitchColumn(int col1,int col2){
		for(int i=0;i<SIZE;i++){
			Cell number1=_numbers[i][col1];
			Cell number2=_numbers[i][col2];
			number1.SetLocation(i,col2);
			number2.SetLocation(i,col1);
			_numbers[i][col1]=number2;
			_numbers[i][col2]=number1;
		}

	}
	//horizontal
	private void SwitchRow(int row1,int row2){
		for(int i=0;i<SIZE;i++){
			Cell number1=_numbers[row1][i];
			Cell number2=_numbers[row2][i];
			number1.SetLocation(row2,i);
			number2.SetLocation(row1,i);
			_numbers[row1][i]=number2;
			_numbers[row2][i]=number1;
		}
	}

	private void GenerateInSimpleMode(){
		for(int i=0;i<9;i++){
			int row1=Random.Range(0,SIZE);
			int row2=Random.Range(0,SIZE);
			SwitchRow(row1,row2);

			int col1=Random.Range(0,SIZE);
			int col2=Random.Range(0,SIZE);
			SwitchColumn(col1,col2);
		}
	}


	private void GenerateInHardMode(){
		for(int i=0;i<9;i++){
			int parentRow=Random.Range(0,3);
			int row1=Random.Range(parentRow*3,parentRow*3+3);
			int row2=Random.Range(parentRow*3,parentRow*3+3);
			SwitchRow(row1,row2);

			int parentCol=Random.Range(0,3);
			int col1=Random.Range(parentCol*3,parentCol*3+3);
			int col2=Random.Range(parentCol*3,parentCol*3+3);
			SwitchColumn(col1,col2);
		}
	}


	private void GenerateUnknownCell(int unknowCnt){
		List<Cell> showedCells=new List<Cell>();
		for(int i=0;i<_numbers.Length;i++){
			Cell[] row=_numbers[i];
			for(int j=0;j<row.Length;j++){
				showedCells.Add(row[j]);
			}
		}

		for(int i=0;i<unknowCnt;i++){
			int rd=Random.Range(0,showedCells.Count);

			bool find=false;
			for(int idx=0;idx<showedCells.Count;idx++){
				Cell cell=showedCells[(rd+idx)%showedCells.Count];
				if(CalculateVaildNumberCount(cell.row,cell.col)==1){
					showedCells.RemoveAt((rd+idx)%showedCells.Count);
					cell.isShow=false;
					find=true;
					break;
				}
			}

			if(!find){
				Debug.LogError("Generate Failed,index="+i);
				break;
			}

		}
	}


	public void Generate(int level){
		GenerateInHardMode();
		GenerateUnknownCell(level);
	}

	private int CalculateVaildNumberCount(int row,int col){
		bool[] invaildNumbers=new bool[9];
		for(int i=0;i<9;i++){
			Cell cell=_numbers[row][i];
			if(cell.isShow&&i!=col){
				invaildNumbers[cell.number-1]=true;
			}
		}

		for(int i=0;i<9;i++){
			Cell cell=_numbers[i][col];
			if(cell.isShow&&i!=row){
				invaildNumbers[cell.number-1]=true;
			}
		}

		int parentRow=row/3;
		int parentCol=col/3;

		for(int i=0;i<9;i++){
			int rowIdx=parentRow*3+i/3;
			int colIdx=parentCol*3+i%3;
			Cell cell=_numbers[rowIdx][colIdx];
			if(cell.isShow&&(rowIdx!=row||colIdx!=col)){
				invaildNumbers[cell.number-1]=true;
			}
		}

		int vaildCnt=0;
		for(int i=0;i<invaildNumbers.Length;i++){
			if(!invaildNumbers[i]){
				vaildCnt++;
			}
		}
		return vaildCnt;
	}

	public int sizeX{
		get{
			return SIZE;
		}
	}

	public int sizeY{
		get{
			return SIZE;
		}
	}

	public Cell this[int i,int j]{
		get{
			return _numbers[i][j];
		}
	}


	public class Cell{

		private int _number;
		private bool _isShow=true;
		private int _userValue=-1;
		private int _row;
		private int _col;
		public System.Action<Cell> onCellUpdate;

		public Cell(int number,int row,int col){
			_number=number;
			_row=row;
			_col=col;
			_userValue=number;
		}

		public void SetLocation(int row,int col){
			_row=row;
			_col=col;
		}

		public void FireUpdate(){
			if(onCellUpdate!=null){
				onCellUpdate(this);
			}
		}

		public int row{
			get{
				return _row;
			}
		}

		public int col{
			get{
				return _col;
			}
		}

		public bool isShow{
			get{
				return _isShow;
			}set{
				_isShow=value;
				if(onCellUpdate!=null){
					onCellUpdate(this);
				}
			}
		}

		public int number{
			get{
				return _number;
			}
		}

		public int userValue{
			get{
				return _userValue;
			}set{
				_userValue=value;
				if(onCellUpdate!=null){
					onCellUpdate(this);
				}
			}
		}

		public bool isVaild{
			get{
				return _number==_userValue;
			}
		}

		public bool CheckVaild(int tryNumber){
			return _number==tryNumber;
		}

	}

	public enum Mode{
		Simple,
		Hard,
	}

}
