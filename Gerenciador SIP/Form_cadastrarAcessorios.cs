using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerenciador_SIP
{
    public partial class Form_cadastrarAcessorios : Form
    {
        List<string> valor_Gerenciador_SIP;
        string login = "";
        public Form_cadastrarAcessorios(List<string> valorGerenciador, string login, string modelo)
        {
            valor_Gerenciador_SIP = valorGerenciador;            
            login = login;
            InitializeComponent();
        }
    }
}
