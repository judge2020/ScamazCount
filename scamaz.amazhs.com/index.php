<?php
  $datas  = parse_ini_string("main.ini", true);
?>
<table border="1" cellspacing="0" cellpadding="5">
  <tbody>
    <?php
      foreach( $datas as $section => $data ) {
    ?>
    <tr>
      <td rowspan="2"><?php echo htmlspecialchars( $data["name"] ); ?></td>
      <td>Name</td>
      <td>Points</td>
	  <td>Total Time</td>
    </tr>
    <tr>
      <td><?php echo htmlspecialchars( $section ); ?></td>
      <td><?php echo htmlspecialchars( $data["Points"] ); ?></td>
	  <td><?php echo htmlspecialchars( $data["Time"] ); ?></td>
      </td>
    </tr>
    <?php
      }
    ?>
  </tbody>
</table>