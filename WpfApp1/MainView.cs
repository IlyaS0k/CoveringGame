using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace WpfApp1
{
    class MainView : INotifyPropertyChanged
    {
        private int _rows = 2;
        private int _columns = 2;
        private bool _isCovered = false;

        private Field _field;
        private Field _prevField = null;
        private Figure[] _figures;

        private ICommand _cellCommand;
        private ICommand _insertCommand;
        private ICommand _rotateCommand;
        public bool IsCovered
        {
            get => _isCovered;
            set
            {
                _isCovered = value;
                OnPropertyChanged("IsCovered");
            }
        }

        public Field Field
        {
            get => _field;
            set
            {
                _field = value;
                OnPropertyChanged("Field");
            }
        }
        public Field PrevField
        {
            get => _prevField;
            set
            {
                _prevField = value;
                OnPropertyChanged("PrevField");
            }
        }
        public Figure[] Figures
        {
            get => _figures;
            set
            {
                _figures = value;
                OnPropertyChanged("Figures");
            }
        }

        public int Rows
        {
            get => _rows;
            set
            {
                _rows = value;
                OnPropertyChanged();
            }
        }
        public int Columns
        {
            get => _columns;
            set
            {
                _columns = value;
                OnPropertyChanged();
            }
        }


        public void GenerateHoles(int count)
        {
            Random random = new Random();
            while (count != 0)
            {
                int row = random.Next(0, Rows);
                int col = random.Next(0, Columns);
                if (Field[row, col].State == State.Empty)
                {
                    count--;
                    Field[row, col].State = State.Hole;
                }
            }
        }
        public void SetFigures()
        {
            Figure[] figures = new Figure[24];

            Figure f0 = new Figure();
            f0.FigureArea[1, 0].State = State.RightBall;
            f0.FigureArea[1, 1].State = State.LeftRightRing; // 0-o-0
            f0.FigureArea[1, 2].State = State.LeftBall;
            f0.Index = 0;

            Figure f1 = new Figure();
            f1.FigureArea[1, 0].State = State.RightHalfRing;
            f1.FigureArea[1, 1].State = State.LeftRightRing; // )-o-0
            f1.FigureArea[1, 2].State = State.LeftBall;
            f1.Index = 1;

            Figure f2 = new Figure();
            f2.FigureArea[1, 0].State = State.RightHalfRing;
            f2.FigureArea[1, 1].State = State.LeftRightRing; // )-o-(
            f2.FigureArea[1, 2].State = State.LeftHalfRing;
            f2.Index = 2;

            Figure f3 = new Figure();
            f3.FigureArea[0, 1].State = State.DownBall;    // 0
            f3.FigureArea[1, 1].State = State.RightUpRing; // o-0
            f3.FigureArea[1, 2].State = State.LeftBall;
            f3.Index = 3;

            Figure f4 = new Figure();
            f4.FigureArea[0, 1].State = State.DownHalfRing;// u
            f4.FigureArea[1, 1].State = State.RightUpRing; // o-0
            f4.FigureArea[1, 2].State = State.LeftBall;
            f4.Index = 4;

            Figure f5 = new Figure();
            f5.FigureArea[0, 1].State = State.DownBall;    // 0
            f5.FigureArea[1, 1].State = State.RightUpRing; // o-c
            f5.FigureArea[1, 2].State = State.LeftHalfRing;
            f5.Index = 5;

            Figure f6 = new Figure();
            f6.FigureArea[0, 1].State = State.DownHalfRing;// u
            f6.FigureArea[1, 1].State = State.RightUpRing; // o-c
            f6.FigureArea[1, 2].State = State.LeftHalfRing;
            f6.Index = 6;

            Figure f7 = new Figure();
            f7.FigureArea[1, 0].State = State.RightHalfRingL;
            f7.FigureArea[1, 1].State = State.LeftRightRing; //u o-c
            f7.FigureArea[1, 2].State = State.LeftHalfRing;
            f7.Index = 7;

            Figure f8 = new Figure();
            f8.FigureArea[1, 0].State = State.RightHalfRingR;
            f8.FigureArea[1, 1].State = State.LeftRightRing; //n o-c
            f8.FigureArea[1, 2].State = State.LeftHalfRing;
            f8.Index = 8;

            Figure f9 = new Figure();
            f9.FigureArea[1, 0].State = State.RightHalfRingL;
            f9.FigureArea[1, 1].State = State.LeftRightRing; //u o-0
            f9.FigureArea[1, 2].State = State.LeftBall;
            f9.Index = 9;

            Figure f10 = new Figure();
            f10.FigureArea[1, 0].State = State.RightHalfRingR;
            f10.FigureArea[1, 1].State = State.LeftRightRing; //n o-0
            f10.FigureArea[1, 2].State = State.LeftBall;
            f10.Index = 10;

            Figure f11 = new Figure();
            f11.FigureArea[0, 1].State = State.DownHalfRingL;//)
            f11.FigureArea[1, 1].State = State.RightUpRing; // o-0
            f11.FigureArea[1, 2].State = State.LeftBall;
            f11.Index = 11;

            Figure f12 = new Figure();
            f12.FigureArea[0, 1].State = State.DownHalfRingR;//(
            f12.FigureArea[1, 1].State = State.RightUpRing; // o-0
            f12.FigureArea[1, 2].State = State.LeftBall;
            f12.Index = 12;

            Figure f13 = new Figure();
            f13.FigureArea[0, 1].State = State.DownBall;    // 0
            f13.FigureArea[1, 1].State = State.RightUpRing; // o-u
            f13.FigureArea[1, 2].State = State.LeftHalfRingL;
            f13.Index = 13;

            Figure f14 = new Figure();
            f14.FigureArea[0, 1].State = State.DownBall;    // 0
            f14.FigureArea[1, 1].State = State.RightUpRing; // o-n
            f14.FigureArea[1, 2].State = State.LeftHalfRingR;
            f14.Index = 14;

            Figure f15 = new Figure();
            f15.FigureArea[0, 1].State = State.DownHalfRingL;//с
            f15.FigureArea[1, 1].State = State.RightUpRing; // o-c
            f15.FigureArea[1, 2].State = State.LeftHalfRing;
            f15.Index = 15;

            Figure f16 = new Figure();
            f16.FigureArea[0, 1].State = State.DownHalfRingR;//)
            f16.FigureArea[1, 1].State = State.RightUpRing; // o-c
            f16.FigureArea[1, 2].State = State.LeftHalfRing;
            f16.Index = 16;

            Figure f17 = new Figure();
            f17.FigureArea[0, 1].State = State.DownHalfRing;// u
            f17.FigureArea[1, 1].State = State.RightUpRing; // o-u
            f17.FigureArea[1, 2].State = State.LeftHalfRingL;
            f17.Index = 17;

            Figure f18 = new Figure();
            f18.FigureArea[0, 1].State = State.DownHalfRingL;//)
            f18.FigureArea[1, 1].State = State.RightUpRing; // o-u
            f18.FigureArea[1, 2].State = State.LeftHalfRingL;
            f18.Index = 18;

            Figure f19 = new Figure();
            f19.FigureArea[0, 1].State = State.DownHalfRingR;//c
            f19.FigureArea[1, 1].State = State.RightUpRing; // o-u
            f19.FigureArea[1, 2].State = State.LeftHalfRingL;
            f19.Index = 19;

            Figure f20 = new Figure();
            f20.FigureArea[0, 1].State = State.DownHalfRing;// u
            f20.FigureArea[1, 1].State = State.RightUpRing; // o-n
            f20.FigureArea[1, 2].State = State.LeftHalfRingR;
            f20.Index = 20;

            Figure f21 = new Figure();
            f21.FigureArea[0, 1].State = State.DownHalfRingL;//)
            f21.FigureArea[1, 1].State = State.RightUpRing; // o-n
            f21.FigureArea[1, 2].State = State.LeftHalfRingR;
            f21.Index = 21;

            Figure f22 = new Figure();
            f22.FigureArea[0, 1].State = State.DownHalfRingR;//c
            f22.FigureArea[1, 1].State = State.RightUpRing; // o-n
            f22.FigureArea[1, 2].State = State.LeftHalfRingR;
            f22.Index = 22;

            Figure f23 = new Figure();
            f23.FigureArea[1, 1].State = State.Hole;// *
            f23.Index = 23;

            figures[0] = f0;
            figures[1] = f1;
            figures[2] = f2;
            figures[3] = f3;
            figures[4] = f4;
            figures[5] = f5;
            figures[6] = f6;
            figures[7] = f7;
            figures[8] = f8;
            figures[9] = f9;
            figures[10] = f10;
            figures[11] = f11;
            figures[12] = f12;
            figures[13] = f13;
            figures[14] = f14;
            figures[15] = f15;
            figures[16] = f16;
            figures[17] = f17;
            figures[18] = f18;
            figures[19] = f19;
            figures[20] = f20;
            figures[21] = f21;
            figures[22] = f22;
            figures[23] = f23;

            Figures = figures;
        }
        public ICommand CellCommand => _cellCommand = new RelayCommand(parameter =>
        {
            Cell cell = (Cell)parameter;
            Cell activeCell = Field.FirstOrDefault(x => x.Active);
            cell.Active = true;
            if (activeCell != null)
            {
                activeCell.Active = false;
            }
        }, parameter => parameter is Cell cell);

        public ICommand RotateCommand => _rotateCommand = new RelayCommand(parameter =>
        {
            int idx = (int)parameter;

            Figure tmp = new Figure();
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    tmp.FigureArea[i, j].State = Figures[idx].FigureArea[i, j].State;
                }
            }

            Figures[idx].FigureArea[0, 2].State = StateRotator(tmp.FigureArea[0, 0].State);
            Figures[idx].FigureArea[1, 2].State = StateRotator(tmp.FigureArea[0, 1].State);
            Figures[idx].FigureArea[2, 2].State = StateRotator(tmp.FigureArea[0, 2].State);
            Figures[idx].FigureArea[0, 1].State = StateRotator(tmp.FigureArea[1, 0].State);
            Figures[idx].FigureArea[1, 1].State = StateRotator(tmp.FigureArea[1, 1].State);
            Figures[idx].FigureArea[2, 1].State = StateRotator(tmp.FigureArea[1, 2].State);
            Figures[idx].FigureArea[0, 0].State = StateRotator(tmp.FigureArea[2, 0].State);
            Figures[idx].FigureArea[1, 0].State = StateRotator(tmp.FigureArea[2, 1].State);
            Figures[idx].FigureArea[2, 0].State = StateRotator(tmp.FigureArea[2, 2].State);


        }, parameter => parameter is int);
        public ICommand InsertCommand => _insertCommand = new RelayCommand(parameter =>
        {
            int idx = (int)parameter;
            Cell activeCell = Field.FirstOrDefault(x => x.Active);
            Figure figure = Figures[idx];
            if (activeCell != null)
            {

                if (IsCanInsert(activeCell, figure))
                {
                    PrevField = new Field(Rows, Columns);
                    for (int i = 0; i < Rows; ++i)
                    {
                        for (int j = 0; j < Columns; ++j)
                        {
                            PrevField[i, j] = new Cell(i, j);
                            PrevField[i, j].State = Field[i, j].State;

                        }
                    }
                    for (int i = 0; i < 3; ++i)
                    {
                        for (int j = 0; j < 3; ++j)
                        {
                            int x = activeCell.Row + i - 1;
                            int y = activeCell.Column + j - 1;
                            Cell cell = figure.FigureArea[i, j];
                            State state = cell.State;
                            if (state == State.Empty) continue;
                            if (Field[x, y].State == State.Empty)
                            {
                                Field[x, y].State = state;
                                continue;
                            }
                            if (Field[x, y].State == State.DownHalfRing || Field[x, y].State == State.UpHalfRing || state == State.DownHalfRing || state == State.UpHalfRing)
                            {
                                Field[x, y].State = State.UpDownCompound;
                            }
                            if (Field[x, y].State == State.LeftHalfRing || Field[x, y].State == State.RightHalfRing || state == State.LeftHalfRing || state == State.RightHalfRing)
                            {
                                Field[x, y].State = State.LeftRightCompound;
                            }
                            if (Field[x, y].State == State.DownHalfRingR || state == State.DownHalfRingR || Field[x, y].State == State.RightHalfRingL || state == State.RightHalfRingL)
                            {
                                Field[x, y].State = State.RightDownCompound;
                            }
                            if (Field[x, y].State == State.DownHalfRingL || state == State.DownHalfRingL || Field[x, y].State == State.LeftHalfRingR || state == State.LeftHalfRingR)
                            {
                                Field[x, y].State = State.LeftDownCompound;
                            }
                            if (Field[x, y].State == State.UpHalfRingR || state == State.UpHalfRingR || Field[x, y].State == State.LeftHalfRingL || state == State.LeftHalfRingL)
                            {
                                Field[x, y].State = State.LeftUpCompound;
                            }
                            if (Field[x, y].State == State.UpHalfRingL || state == State.UpHalfRingL || Field[x, y].State == State.RightHalfRingR || state == State.RightHalfRingR)
                            {
                                Field[x, y].State = State.RightUpCompound;
                            }
                        }
                    }
                    
                    if (Field.FirstOrDefault(x => x.State == State.Empty) == null)
                    {
                        IsCovered = true;
                    }
                }
            }

        }, parameter => parameter is int);

        public bool IsCanInsert(Cell activeCell, Figure figure)
        {

            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    int x = activeCell.Row + i - 1;
                    int y = activeCell.Column + j - 1;
                    Cell cell = figure.FigureArea[i, j];
                    State state = cell.State;
                    if (state == State.Empty) continue;
                    if (x < 0 || y < 0) return false;
                    if (x >= Rows || y >= Columns) return false;
                    if (StateSwitcher(state, Field[x, y].State) == false && StateSwitcher(Field[x, y].State, state) == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        bool StateSwitcher(State state, State insertedState)
        {
            switch (insertedState)
            {
                case State.Empty:
                    {
                        return true;
                    }

                case State.DownHalfRing:
                    {
                        if (state != State.UpBall)
                            return false;
                        else
                            return true;
                    }
                case State.DownHalfRingL:
                    {
                        if (state != State.LeftBall)
                            return false;
                        else
                            return true;
                    }
                case State.DownHalfRingR:
                    {
                        if (state != State.RightBall)
                            return false;
                        else
                            return true;
                    }

                case State.LeftHalfRing:
                    {
                        if (state != State.RightBall)
                            return false;
                        else
                            return true;
                    }

                case State.LeftHalfRingL:
                    {
                        if (state != State.UpBall)
                            return false;
                        else
                            return true;
                    }
                case State.LeftHalfRingR:
                    {
                        if (state != State.DownBall)
                            return false;
                        else
                            return true;
                    }

                case State.RightHalfRing:
                    {
                        if (state != State.LeftBall)
                            return false;
                        else
                            return true;

                    }
                case State.RightHalfRingL:
                    {
                        if (state != State.DownBall)
                            return false;
                        else
                            return true;

                    }
                case State.RightHalfRingR:
                    {
                        if (state != State.UpBall)
                            return false;
                        else
                            return true;

                    }
                case State.UpHalfRing:
                    {
                        if (state != State.DownBall)
                            return false;
                        else
                            return true;
                    }
                case State.UpHalfRingL:
                    {
                        if (state != State.RightBall)
                            return false;
                        else
                            return true;
                    }
                case State.UpHalfRingR:
                    {
                        if (state != State.LeftBall)
                            return false;
                        else
                            return true;
                    }


                default: { return false; }

            }
        }
        State StateRotator(State state)
        {
            switch (state)
            {
                case State.UpBall: return State.RightBall;

                case State.LeftBall: return State.UpBall;

                case State.DownBall: return State.LeftBall;

                case State.RightBall: return State.DownBall;

                case State.UpHalfRing: return State.RightHalfRing;

                case State.LeftHalfRing: return State.UpHalfRing;

                case State.DownHalfRing: return State.LeftHalfRing;

                case State.RightHalfRing: return State.DownHalfRing;

                case State.LeftRightRing: return State.UpDownRing;

                case State.UpDownRing: return State.LeftRightRing;

                case State.LeftUpRing: return State.RightUpRing;

                case State.LeftDownRing: return State.LeftUpRing;

                case State.RightDownRing: return State.LeftDownRing;

                case State.RightUpRing: return State.RightDownRing;

                case State.DownHalfRingL: return State.LeftHalfRingL;

                case State.DownHalfRingR: return State.LeftHalfRingR;

                case State.LeftHalfRingL: return State.UpHalfRingL;

                case State.LeftHalfRingR: return State.UpHalfRingR;

                case State.RightHalfRingL: return State.DownHalfRingL;

                case State.RightHalfRingR: return State.DownHalfRingR;

                case State.UpHalfRingL: return State.RightHalfRingL;

                case State.UpHalfRingR: return State.RightHalfRingR;

                default: return state;
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
