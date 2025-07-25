module com.example.memseri {
    requires javafx.controls;
    requires javafx.fxml;


    opens com.example.memseri to javafx.fxml;
    exports com.example.memseri;
    exports com.example.memseri.controller;
    opens com.example.memseri.controller to javafx.fxml;
}