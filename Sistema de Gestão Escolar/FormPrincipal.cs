using System.Drawing.Drawing2D;

namespace Sistema_de_Gestão_Escolar
{
    public partial class FormPrincipal : Form
    {
        private GestorEscola gestor; // Instância única do gestor

        public FormPrincipal()
        {
            InitializeComponent();
            gestor = new GestorEscola(); // Criar apenas um gestor para partilhar entre os formulários
        }
        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            // Definir botões redondos
            SetRoundButton(btnTurmas);
            SetRoundButton(btnProfessores);
            SetRoundButton(btnNotas);
            SetRoundButton(btnDisciplinas);
            SetRoundButton(btnAlunos);
            SetRoundButton(btnSair);
            SetRoundButton(btnCreditos);
            SetRoundButton(btnEvento);
            SetRoundButton(btnFormRelatorio);
            SetRoundButton(btnFormHorario);
         
        }

        private void SetRoundButton(Button button)
        {
            // Cria um caminho gráfico para o botão
            GraphicsPath path = new GraphicsPath();

            // Define um retângulo arredondado para o botão
            path.AddEllipse(0, 0, button.Width, button.Height);

            // Atribui a região do botão para o caminho arredondado
            button.Region = new Region(path);

            // Opcional: Define a cor de fundo e borda
            button.BackColor = Color.LightBlue;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
        }

        private void btnTurmas_Click(object sender, EventArgs e)
        {
            FormTurma formTurmas = new FormTurma(gestor);
            formTurmas.Show();
        }

        private void btnProfessores_Click(object sender, EventArgs e)
        {
            FormProfessor formProfessores = new FormProfessor(gestor);
            formProfessores.Show();
        }

        private void btnNotas_Click(object sender, EventArgs e)
        {
            FormNota formNotas = new FormNota(gestor);
            formNotas.Show();
        }

        private void btnDisciplinas_Click(object sender, EventArgs e)
        {
            FormDisciplina formDisciplinas = new FormDisciplina(gestor);
            formDisciplinas.Show();
        }

        private void btnAlunos_Click(object sender, EventArgs e)
        {
            FormAluno formAlunos = new FormAluno(gestor);
            formAlunos.Show();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Fecha o programa
        }

        private void btnCreditos_Click(object sender, EventArgs e)
        {
            FormCredito formCredito = new FormCredito();

            formCredito.Show();
        }

        private void btnEvento_Click(object sender, EventArgs e)
        {
            FormEvento formEvento = new FormEvento(gestor);
            formEvento.Show();
        }

        private void btnFormRelatorio_Click(object sender, EventArgs e)
        {

            FormRelatorio formRelatorio = new FormRelatorio(gestor);
            formRelatorio.Show();
        }

        private void btnFormHorario_Click(object sender, EventArgs e)
        {
            FormHorario formHorario = new FormHorario(gestor);
            formHorario.Show();
        }

      
    }
}
