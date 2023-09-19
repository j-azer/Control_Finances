# Projeto C# Money Plus
<p>
 <h3>Resumo do Projeto:</h3> Este aplicativo, além de consumir uma API externa de notícias sobre o mercado financeiro que alimenta uma das home e enviar diáriamente e-mail ao usuário com seu resumo patrimonial atualizado, também tem o controle de receita e despesas, que permite o usuário ter diversas contas e transferir diferentes importâncias de valor entre elas, lançar despesas e atribuir a qual conta sai esta despesa. Esta App também disponibiliza alguns relatórios importantes para o usuário, e o principal deles é o de Lastro Patrimonial, onde o usuário define uma taxa média de inflação e uma taxa média de retorno dos seus investimentos e o sistema calcula, baseado em sua despesa média, quanto tempo o usuário pode estar em casa sem trabalhar.
<p>
 <h3>Informações do Projeto:</h3>
<p>
Os dados da DB, estão em um arquivo de excel denominado <b>DB_MoneuPlus.xls</b> e disponíveis no e-mail que foi enviado e no repositório que foi compartilhado no link acima.
<p>
O Sistema de envio de e-mail foi testado com o tempo de 50 segundos funcionando perfeitamente e com isso fiz a alteração para 1 dia, o que é proposto para a realidade da aplicação. Caso deseje alterar para fins de teste, este código se encontra na pasta Services / Classe EmailBackgroundService / linha 9. Sugiro Também a substituição do e-mail fake(user@gmail.com) por um e-mail válido para o teste e a alteração deverá ser feita na coluna "Email" na DB_MoneyPlus e na dbo.AspNetUsers, aconselho também ir ao <b>appsettings.json</b> e efetuar a configuração do e-mail que será criado para que possa testar e ou utilizar a App em relação a função de envio de e-mail diário ao usuário com o resumo patrimonial atualizado.
<p>
Em breve esta App terá o consumo de uma API na Home Page. Onde o usuário terá informação atual das principais fontes de notícias sobre o mercados financeiro e finanças em geral. 
<p>
Abaixo seguem dados importantes de acesso de usuários e administrador para acesso e testes caso queiram utilizar a DB em anexo, lembro que não são e-mails válidos:
<p>
________________________________
<p>
UserEmail: user@gmail.com
<p>
UserPwd: User@123456
<p>
________________________________
<p>
AdminEmail: admin@moneyplus.com
<p>
AdminPwd: Admin@123456
<p>
________________________________
<p>
Existe também um acesso "secreto"(que agora não é mais !!!), onde o usuário é levado a uma Home Page e lá ele consegue se tornar automaticamente um Role Admin, inserindo seu e-mail e uma senha válida, nos moldes que foram criadas acima. Após a numeração da porta é só colocar <b>/admin/secret/?pwd=pouco-segura</b> e terá acesso a esta página e poderá ter o role de admin e seus acessos.
<p>
<p>
<p>
 <h3>Bibliografia:</h3>
<p>
<p>
 <h4>Na confecção geral do Projeto:</h4> 
<p>
A maioria das consultas foram feitas à literaturas de C Sharp, porém, também achei necessário colocar todos os links dos locais por onde passei na web, para que sirva de consulta futura, organização do projeto e confirmação de informações com mais facilidade.
<p>
<p>
https://datatables.net/
<p>
https://youtu.be/gfZkMG-jGqM
<p>
https://youtu.be/EyLtPcaCm4s
<p>
https://youtu.be/XDjePB9vOF0
<p>
https://youtu.be/URotHvDv_Ks
<p>
https://youtu.be/kF73sl4gUto
<p>
https://youtu.be/qvdG_tl6D6c
<p>
https://youtu.be/_F78Secq5ak
<p>
https://youtu.be/TXJy8R1OH9A
<p>
https://youtu.be/IKSwTJVOeHM
<p>
https://youtu.be/J17876qZUH0
<p>
https://youtu.be/dGL1ghDGjg4
<p>
https://youtu.be/7FZta079-uE
<p>
https://youtu.be/yZN086NPTG4
<p>
https://youtu.be/lSwFSSqpwCE
<p>
https://youtu.be/CQcrffbU7iU
<p>
https://youtu.be/lQRyQcUlOhs
<p>
https://youtu.be/a3S4mQ16QNs
<p>
<p>
<p>
<p>
<p>
<p>
<p>
<p>
 <h4>Na confecção da view RoleAdmin:</h4> 
<p>
https://youtu.be/ZTjGwat5mro
<p>
https://youtu.be/sO8zUaWvOxo
<p>
https://youtu.be/IBpZCFbjvA0
<p>
https://youtu.be/TuJd2Ez9i3I
<p>
<p>
<p>
<p>
 <h4>Na confecção do envio de E-mail:</h4> 
<p>
https://youtu.be/PvO_1T0FS_A
<p>
https://youtu.be/qzummN6Xx9Q
<p>
https://youtu.be/a3S4mQ16QNs
<p>
https://youtu.be/9WnIGwBitbw
<p>
https://youtu.be/OGuQu13OiZk
<p>
https://youtu.be/ohV_BKXhHSI
<p>
<p>
<p>
<p>
 <h4>Na confecção da API de Notícias Financeiras:</h4> 
<p>
https://youtu.be/MJEOPCEZW24
<p>
<p>
<p>
<p>
<p>