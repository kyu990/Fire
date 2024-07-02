using MobleFinal._Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobleFinal
{
    public partial class MainAdminForm : Form
    {
        private List<Label> menuList;
        private List<Color> menuColorList;

        // 초기 윈폼 크기
        private Size originalFormSize = new Size(1600, 899);

        // 초기 배경 이미지 크기
        private Size originalBackgroundSize;
        private Dictionary<Control, Size> originalControlSizes = new Dictionary<Control, Size>();
        private Dictionary<Control, Point> originalControlLocations = new Dictionary<Control, Point>();

        private TreeView tr_cctv;
        private TreeView tr_exit;

        public MainAdminForm()
        {
            InitializeComponent();
            menuList = new List<Label>();
            menuColorList = new List<Color>();
            originalBackgroundSize = this.BackgroundImage.Size;
            TreeViews();
            
        }

        private void TreeViews()
        {
            // tr_cctv 초기화
            tr_cctv = new TreeView
            {
                BackColor = Color.FromArgb(43, 55, 72),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.None,
                Location = new Point(0, 371),
                Size = new Size(245, 54),
                Visible = false
            };
            // tr_cctv에 노드 추가
            TreeNode cctvNode = tr_cctv.Nodes.Add("CCTV");
            cctvNode.Nodes.Add("실시간 영상확인");
            cctvNode.Nodes.Add("영상 기록확인");
            // tr_cctv 컨트롤 추가
            this.Controls.Add(tr_cctv);

            // tr_cctv 노드 클릭 이벤트 처리
            tr_cctv.NodeMouseClick += (sender, e) =>
            {
                // 노드의 텍스트에 따라 다른 동작 수행
                switch (e.Node.Text)
                {
                    case "실시간 영상확인":
                        // 탭 컨트롤의 특정 탭 페이지를 활성화
                        Tab_Main.SelectedTab = tabPage2;
                            foreach (Label button in menuList)
                            {
                                if (button == btn_cctv)
                                {
                                    // 선택된 버튼의 배경색 변경
                                    button.ForeColor = Color.FromArgb(0, 128, 224);
                                    // Tab_Menu_Bar를 선택된 버튼과 동일한 위치로 이동
                                    Tab_Menu_Bar.Location = new Point(button.Location.X, 0);
                                    button.Visible = true;
                                    Tab_Menu_Bar.Visible = true;
                                }
                                else
                                {
                                    // 선택된 버튼 이외의 버튼은 하얀색으로 설정
                                    button.ForeColor = Color.White;
                                    button.Visible = false;
                                }
                            }
                        break;
                    case "영상 기록확인":
                        // 탭 컨트롤의 특정 탭 페이지를 활성화
                        Tab_Main.SelectedTab = tabPage3;
                            foreach (Label button in menuList)
                            {
                                if (button == cctv_video)
                                {
                                    // 선택된 버튼의 배경색 변경
                                    button.ForeColor = Color.FromArgb(0, 128, 224);
                                    // Tab_Menu_Bar를 선택된 버튼과 동일한 위치로 이동
                                    Tab_Menu_Bar.Location = new Point(button.Location.X, 0);
                                    button.Visible = true;
                                    Tab_Menu_Bar.Visible = true;
                                }
                                else
                                {
                                    // 선택된 버튼 이외의 버튼은 하얀색으로 설정
                                    button.ForeColor = Color.White;
                                    button.Visible = false;
                                }
                            }
                        break;
                    default:
                        break;
                }
            };

            // tr_exit 초기화
            tr_exit = new TreeView
            {
                BackColor = Color.FromArgb(43, 55, 72),
                BorderStyle = BorderStyle.None,
                ForeColor = Color.White,
                Location = new Point(0, 547),
                Size = new Size(245, 54),
                Visible = false
            };
            // tr_exit에 노드 추가
            TreeNode exitNode = tr_exit.Nodes.Add("Exit");
            exitNode.Nodes.Add("로그아웃");
            exitNode.Nodes.Add("종료");
            // tr_exit 컨트롤 추가
            this.Controls.Add(tr_exit);

            // tr_exit 노드 클릭 이벤트 처리
            tr_exit.NodeMouseClick += (sender, e) =>
            {
                // 노드의 텍스트에 따라 다른 동작 수행
                switch (e.Node.Text)
                {
                    case "로그아웃":
                        LoginForm loginForm = new LoginForm();
                        loginForm.Show();
                        //this.Hide();
                        break;
                    case "종료":
                        this.Close();
                        break;
                    default:
                        break;
                }
            };
        }
       
        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                originalControlSizes.Add(control, control.Size);
                originalControlLocations.Add(control, control.Location); // 각 컨트롤의 초기 위치 저장
            }

            SetBtncolor();
            SetTappage();
            LoadFormTabControl();
        }

        private void LoadFormTabControl()
        {
            // AdminForm 인스턴스 생성
            AdminForm adminForm = new AdminForm();
            FireDetectForm fireDetectForm = new FireDetectForm();
            VideoLog videolog = new VideoLog();


            // AdminForm의 속성 설정
            adminForm.TopLevel = false;
            adminForm.FormBorderStyle = FormBorderStyle.None;
            adminForm.Dock = DockStyle.Fill;

            fireDetectForm.TopLevel = false;
            fireDetectForm.FormBorderStyle = FormBorderStyle.None;
            fireDetectForm.Dock = DockStyle.Fill;

            videolog.TopLevel = false;
            videolog.FormBorderStyle = FormBorderStyle.None;
            videolog.Dock = DockStyle.Fill;

            // tabPage4에 AdminForm 추가
            tabPage4.Controls.Add(adminForm);
            tabPage2.Controls.Add(fireDetectForm);
            tabPage3.Controls.Add(videolog);
            // AdminForm 표시
            adminForm.Show();
            fireDetectForm.Show();
            videolog.Show();
        }

        private void SetBtncolor()
        {
            menuList.AddRange(new[] { btn_main, btn_cctv, cctv_video, btn_admin, btn_user });
            menuColorList.AddRange(Enumerable.Repeat(Color.FromArgb(0, 128, 224), menuList.Count));
        }

        private void SetTappage()
        {
            // 시작 TabPage 설정
            Tab_Main.SelectedIndex = 0;

            // 초기 탭 색상 설정
            menuList[0].ForeColor = menuColorList[0];
            Tab_Menu_Bar.BackColor = menuColorList[0];
            Tab_Menu_Bar.Location = new Point(menuList[0].Location.X, 0);
        }

        private void setMenuChgane(int index)
        {
            if (index >= 0 && index < menuList.Count) // 메뉴 목록의 범위를 확인
            {
                if (Tab_Main.SelectedIndex != index)
                {
                    menuList[Tab_Main.SelectedIndex].ForeColor = Color.White;
                    menuList[index].ForeColor = menuColorList[index];
                    Tab_Menu_Bar.BackColor = menuColorList[index];
                    Tab_Menu_Bar.Location = new Point(menuList[index].Location.X, 0);
                    Tab_Main.SelectedIndex = index;
                }
            }
            else
            {
                MessageBox.Show("처리 할 수 없는 인덱스입니다.");
            }
        }


        private void btn_main_Click(object sender, EventArgs e)
        {
            setMenuChgane(0);
        }

        private void btn_cctv_Click(object sender, EventArgs e)
        {
            setMenuChgane(1);
        }

        private void cctv_video_Click(object sender, EventArgs e)
        {
            setMenuChgane(2);
        }

        private void btn_admin_Click(object sender, EventArgs e)
        {
            setMenuChgane(3);
        }

        private void btn_user_Click(object sender, EventArgs e)
        {
            setMenuChgane(4);
        }

        private void bt_ex_Click(object sender, EventArgs e)
        {
            bt_ex.FlatAppearance.MouseDownBackColor = Color.Transparent;
            // 창 닫기
            this.Close();
        }

        private void bt_cl_Click(object sender, EventArgs e)
        {
            bt_cl.FlatAppearance.MouseDownBackColor = Color.Transparent;
            // 창 최소화
            this.WindowState = FormWindowState.Minimized;
        }

        private void bt_full_Click(object sender, EventArgs e)
        {
            //사용 금지

            bt_full.FlatAppearance.MouseDownBackColor = Color.Transparent;
            if (this.WindowState == FormWindowState.Maximized)
            {
                // 창을 원래 크기로 전환
                this.WindowState = FormWindowState.Normal;
                OriginalSize();
            }
            else
            {
                MaxScreen();
            }
        }
        
        private void MaxScreen()
        {
            // 현재 모니터의 작업 영역 크기를 가져와서 창을 그 크기로 최대화
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Size = workingArea.Size;
            this.Location = workingArea.Location;
            this.WindowState = FormWindowState.Maximized;
            SizeControls();
            BackgroundSize();
        }

        private void OriginalSize()
        {
            // 창을 초기 크기로 복원
            this.Size = originalFormSize;
            // 배경 이미지 크기도 초기 크기로 복원
            if (this.BackgroundImage != null)
            {
                this.BackgroundImage = ResizeImage(this.BackgroundImage, originalBackgroundSize);
            }

            SizeControls();

            // 각 컨트롤의 원래 크기 및 위치로 복원
            foreach (var item in originalControlSizes)
            {
                item.Key.Size = item.Value;
                item.Key.Location = originalControlLocations[item.Key]; // 원래 위치로 이동
            }
        }


        private void SizeControls()
        {
            // 내부 컨트롤의 크기와 위치를 조정하여 화면에 맞게 자동으로 늘립니다.
            foreach (Control control in this.Controls)
            {
                // 각 컨트롤의 크기와 위치를 화면에 맞게 조정합니다.
                control.Size = new Size((int)(control.Width * (float)this.Width / originalFormSize.Width),
                                        (int)(control.Height * (float)this.Height / originalFormSize.Height));

                control.Location = new Point((int)(originalControlLocations[control].X * (float)this.Width / originalFormSize.Width),
                                             (int)(originalControlLocations[control].Y * (float)this.Height / originalFormSize.Height));
            }
        }

        private void BackgroundSize()
        {
            // 배경 이미지 크기를 화면에 맞게 조정
            if (this.BackgroundImage != null)
            {
                this.BackgroundImage = ResizeImage(this.BackgroundImage, this.Size);
            }

        }

        private Bitmap ResizeImage(Image image, Size size)
        {
            // 이미지 크기를 조정하여 반환
            return new Bitmap(image, size);
        }

        private void bt_ex_MouseHover(object sender, EventArgs e)
        {
            bt_ex.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }

        private void bt_cl_MouseHover(object sender, EventArgs e)
        {
            bt_cl.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }

        private void bt_full_MouseHover(object sender, EventArgs e)
        {
            bt_full.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }

        private void Cctv_bt_Click(object sender, EventArgs e)
        {
            tr_cctv.Visible = !tr_cctv.Visible;

            ButtonShowLocationms();

        }

        private void Exitbt_Click(object sender, EventArgs e)
        {
            tr_exit.Visible = !tr_exit.Visible;
            ButtonShowLocationms();
        }

        private void ButtonShowLocationms()
        {
            int offset = tr_cctv.Visible ? tr_cctv.Height : 0;
            Adminbt.Location = new Point(0, tr_cctv.Visible ? tr_cctv.Location.Y + tr_cctv.Size.Height : Cctv_bt.Location.Y + Cctv_bt.Height);
            Exitbt.Location = new Point(0, Adminbt.Location.Y + Adminbt.Size.Height);
            tr_exit.Location = new Point(0, Exitbt.Location.Y + Exitbt.Size.Height);
        }

        //private void Main_bt_Click(object sender, EventArgs e)
        //{
        //    Tab_Main.SelectedTab = tabPage1;
        //    setMenuChgane(0);
        //}

        //private void Adminbt_Click(object sender, EventArgs e)
        //{
        //    Tab_Main.SelectedTab = tabPage4;
        //    setMenuChgane(3);
        //}

        //private void Userbt_Click(object sender, EventArgs e)
        //{
        //    Tab_Main.SelectedTab = tabPage5;
        //    setMenuChgane(4);
        //}

        private void Main_bt_Click(object sender, EventArgs e)
        {
            Tab_Main.SelectedTab = tabPage1;
            

            // menuList에 저장된 버튼들에 대한 작업 수행
            foreach (Label button in menuList)
            {
                if (button == btn_main)
                {
                    // 선택된 버튼의 배경색 변경
                    button.ForeColor = Color.FromArgb(0, 128, 224);
                    // Tab_Menu_Bar를 선택된 버튼과 동일한 위치로 이동
                    Tab_Menu_Bar.Location = new Point(button.Location.X, 0);
                    button.Visible = true;
                    Tab_Menu_Bar.Visible = true;
                }
                else
                {
                    // 선택된 버튼 이외의 버튼은 하얀색으로 설정
                    button.ForeColor = Color.White;
                    button.Visible = false;
                }
            }
        }

        private void Adminbt_Click(object sender, EventArgs e)
        {
            Tab_Main.SelectedTab = tabPage4;
            

            // menuList에 저장된 버튼들에 대한 작업 수행
            foreach (Label button in menuList)
            {
                if (button == btn_admin)
                {
                    // 선택된 버튼의 배경색 변경
                    button.ForeColor = Color.FromArgb(0, 128, 224);
                    // Tab_Menu_Bar를 선택된 버튼과 동일한 위치로 이동
                    Tab_Menu_Bar.Location = new Point(button.Location.X, 0);
                    button.Visible = true;
                }
                else
                {
                    // 선택된 버튼 이외의 버튼은 하얀색으로 설정
                    button.ForeColor = Color.White;
                    button.Visible = false;                 
                }
            }
        }

        private void Userbt_Click(object sender, EventArgs e)
        {
            Tab_Main.SelectedTab = tabPage5;
           

            // menuList에 저장된 버튼들에 대한 작업 수행
            foreach (Label button in menuList)
            {
                if (button == btn_user)
                {
                    // 선택된 버튼의 배경색 변경
                    button.ForeColor = Color.FromArgb(0, 128, 224);
                    // Tab_Menu_Bar를 선택된 버튼과 동일한 위치로 이동
                    Tab_Menu_Bar.Location = new Point(button.Location.X, 0);
                    button.Visible = true;
                    Tab_Menu_Bar.Visible = true;
                }
                else
                {
                    // 선택된 버튼 이외의 버튼은 하얀색으로 설정
                    button.ForeColor = Color.White;
                    button.Visible = false;
                }
            }
        }

    }
}