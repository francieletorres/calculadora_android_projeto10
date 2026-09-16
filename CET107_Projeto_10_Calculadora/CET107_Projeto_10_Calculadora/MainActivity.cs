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
        private TextView calculator_text_viewC;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            calculator_text_viewC = FindViewById<TextView>(Resource.Id.calculator_text_view);


        }

        //ButtonClick()
        [Java.Interop.Export("ButtonClick")]
        public void ButtonClick(Android.Views.View view)
        {
            //obter o botao que foi clicado
            Button button = (Button)view;

            if("0123456789.".Contains(button.Text))
            {
                operandos[0] += button.Text;

                calculator_text_viewC.Text = operandos[0];
            }
            else if ("+-x/".Contains(button.Text))
            {
                operacao = button.Text;

                operandos[1] = operandos[0];
                operandos[0] = "";
            }
            else if(button.Text == "=")
            {
                double resultado = 0;
                double op1 = double.Parse(operandos[1]);
                double op2 = double.Parse(operandos[0]);

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
                
                calculator_text_viewC.Text = resultado.ToString();

                operandos[0] = "";
                operandos[1] = "";
                operacao = "";
            }
            else if(button.Text == "DEL")
            {
                operandos[0] = "";
                operandos[1] = "";
                operacao = "";
                calculator_text_viewC.Text = "0";

            }

        }
    }
}