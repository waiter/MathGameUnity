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


	private void GenerateUnknownCell(int unknowCnt,int depth){
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
				if(CheckCanBeRemoved(cell.row,cell.col,depth)){
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


	public void Generate(int level,int depth=1){
		GenerateInHardMode();
		GenerateUnknownCell(level,depth);
	}

	private bool CheckCanBeRemoved(int row,int col,int depth){
		HashSet<int> sets=CalculateVaildNumbers( row, col);
		if(sets.Count==0){
			return false;
		}
		if(sets.Count==1){
			return true;
		}
		if(depth>0){
			Cell cell=_numbers[row][col];
			int vaildCnt=0;
			foreach(int n in sets){
				cell.userValue=n;
				bool findError=false;
				ForeachRelativeCell(row,col,delegate(Cell c) {
					if(!c.isShow){
						if(!CheckCanBeRemoved(c.row,c.col,depth-1)){
							findError=true;
							return true;
						}
					}
					return false;
				});

				cell.userValue=cell.number;
				if(!findError){
					vaildCnt++;
				}
			}
			if(vaildCnt>1){
				return false;
			}
		}
		return true;

	}

	private void ForeachRelativeCell(int row,int col,System.Func<Cell,bool> onVisitor){
		for(int i=0;i<9;i++){
			Cell cell=_numbers[row][i];
			if(i!=col){
				if(onVisitor(cell)){
					return;
				}
			}
		}
		
		for(int i=0;i<9;i++){
			Cell cell=_numbers[i][col];
			if(i!=row){
				if(onVisitor(cell)){
					return;
				}
			}
		}
		
		int parentRow=row/3;
		int parentCol=col/3;
		
		for(int i=0;i<9;i++){
			int rowIdx=parentRow*3+i/3;
			int colIdx=parentCol*3+i%3;
			Cell cell=_numbers[rowIdx][colIdx];
			if((rowIdx!=row||colIdx!=col)){
				if(onVisitor(cell)){
					return;
				}
			}
		}
	}

	private HashSet<int> CalculateVaildNumbers(int row,int col){

		bool[] invaildNumbers=new bool[9];
		ForeachRelativeCell(row,col,delegate(Cell cell) {
			if(cell.isShow){
				invaildNumbers[cell.number-1]=true;
			}
			return false;
		});

		HashSet<int> ret=new HashSet<int>();
		for(int i=0;i<invaildNumbers.Length;i++){
			if(!invaildNumbers[i]){
				ret.Add(i+1);
			}
		}
		return ret;
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
		private set{
			_numbers[i][j]=value;
		}
	}


	public byte[] Save(){
		System.Text.StringBuilder builder=new System.Text.StringBuilder();
		for(int i=0;i<sizeX;i++){
			for(int j=0;j<sizeY;j++){
				Cell cell=this[i,j];
				builder.Append(cell.isShow);
				builder.Append(',');
				builder.Append(cell.userValue);
				builder.Append(',');
				builder.Append(cell.number);
				builder.Append('|');
			}
		}
		return System.Text.Encoding.UTF8.GetBytes(builder.ToString());
	}

	public void Load(byte[] binary){
		string str=System.Text.Encoding.UTF8.GetString(binary);
		string[] cellStr=str.Split('|');
		for(int i=0;i<cellStr.Length;i++){
			if(string.IsNullOrEmpty(cellStr[i])){
				continue;
			}
			string[] fields=cellStr[i].Split(',');
			bool isShow=bool.Parse(fields[0]);
			int userValue=int.Parse(fields[1]);
			int number=int.Parse(fields[2]);

			int row=i/SIZE;
			int col=i%SIZE;
			this[row,col]=new Cell(number,row,col);
			this[row,col].userValue=userValue;
			this[row,col].isShow=isShow;
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
