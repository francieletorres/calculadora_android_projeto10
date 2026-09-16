namespace CET107_Projeto_10_Calculadora
{
    [Activity(Label = "@string/app_name", 
        Icon = "@drawable/icon_calculadora",
        Theme ="@style/AppTheme",
        MainLauncher = true)]
    public class MainActivity : Activity
    {
        //Dados membros da classe "mainActivity
        //Entradas
        private string[] operandos = new string[2];

        private string operacao = "";

        //saidas
        private TextView calculator_text_viewC = null!;

        private ImageButton? ibSairC = null;


        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            calculator_text_viewC = FindViewById<TextView>(Resource.Id.calculator_text_view)
                ?? throw new InvalidOperationException("TextView não encontrada");

            ibSairC = FindViewById<ImageButton>(Resource.Id.ibSair);

            if (ibSairC != null)
            {
                ibSairC.Click += (sender, e) =>
                {
                    VerificaSaida();
                   
                    //Finish(); // Fecha a atividade atual, encerrando a aplicação.

                };
            }
        }

        private void VerificaSaida()
        {
            if (this == null) return;

            var builder = new Android.App.AlertDialog.Builder(this);
            builder.SetTitle("Sair");
            builder.SetMessage("Tem certeza que deseja sair?");
            builder.SetPositiveButton("Sim", (sender, e) =>
            {
                Finish();
            });
            builder.SetNegativeButton("Não", (sender, e) =>
            {

            });
            builder.Show();

        }

        //ButtonClick()
        [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage(
            "triming",
            "IL2026",
            Justification ="Método chamado via android:onlick em layout XML!")]
        [Java.Interop.Export("ButtonClick")]
        public void ButtonClick(Android.Views.View view)
        {
            //obter o botao que foi clicado
            Button button = (Button)view;
            string texto = button.Text?.ToString() ?? "";

            if("0123456789.".Contains(texto))
            {
                AdicionaDigitoOuPontoDecimal(texto);
            }
            else if ("+-x/".Contains(texto))
            {
               AdicionaOperador(texto);
            }
            else if(texto == "=")
            {
                CalcularResultado();
                operacao = "";
                AtualizaCaixaDoResultado();
            }
            else if(texto == "DEL")
            {
                LimpaParametros();
                AtualizaCaixaDoResultado();

            }

        }


        //Método auxiliar para armazenar a operacao selecionada pelo utilizador
        //e para preparar para a entrada do segundo operando
        private void AdicionaOperador(string operador)
        {
            if (operandos[0] == "")
            {
                return;
            }
            else if(operandos[1] != "")
            {
                CalcularResultado();
                operacao = operador;
               
            }
            else
            {
                operacao = operador;
            }

            AtualizaCaixaDoResultado();
        }



        private void AdicionaDigitoOuPontoDecimal(string texto)
        {
            int numOperando = (operacao == null || operacao == "") ? 0 : 1;

            if(texto == "." && operandos[numOperando].Contains("."))
            {
                return;
            }

            operandos[numOperando] += texto;

            AtualizaCaixaDoResultado();
        }


        //Obtem e apresenta o resultado da operacao atual
        private void CalcularResultado()
        {
            double? resultado = null;

            double? primeiroOperando = double.TryParse(operandos[0], out double op1) ? op1 : (double?)null;
            double? segundoOperando = double.TryParse(operandos[1], out double op2) ? op2 : (double?)null;

            switch (operacao)
            {
                case "+":
                    resultado = op1 + op2;
                    break;
                case "-":
                    resultado = op1 - op2;
                    break;
                case "x":
                    resultado = op1 * op2;
                    break;
                case "/":
                    if (op2 != 0)
                        resultado = op1 / op2;
                    else
                        calculator_text_viewC.Text = "Erro: Divisão por zero";
                    break;
            }

            if (resultado.HasValue)
            {
                operandos[0] = resultado.Value.ToString();
                operandos[1] = "";

            }

        }

        private void LimpaParametros()
        {
            operandos[0] = "";
            operandos[1] = "";
            operacao = "";

        }

        private void AtualizaCaixaDoResultado()
        {
            calculator_text_viewC.Text = operandos[0] + " " + operacao + " " + operandos[1];
        }

    }
}