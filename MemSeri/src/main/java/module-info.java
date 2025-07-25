module com.example.memseri {
    requires javafx.controls;
    requires javafx.fxml;


    opens com.example.memseri to javafx.fxml;
    exports com.example.memseri;
}